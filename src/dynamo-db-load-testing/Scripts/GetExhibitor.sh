#!/bin/bash

# Base URL for the endpoint
BASE_URL="https://leads-shared-service-perf.css.rxweb-dev.com/v1/exhibitors"
QUERY_PARAM="includePublishedProductDocuments=true"

# List of exhibitor IDs
EXHIBITOR_IDS=(
    "exh-perf-test-AUTOM23-42"
    "exh-perf-test-AUTOM23-37"
    "exh-perf-test-AUTOM23-30"
    "exh-perf-test-AUTOM23-34"
    "exh-perf-test-AUTOM23-29"
    "exh-perf-test-AUTOM23-32"
    "exh-perf-test-AUTOM23-44"
    "exh-perf-test-AUTOM23-35"
    "exh-perf-test-AUTOM23-28"
    "exh-perf-test-AUTOM23-41"
    "exh-perf-test-AUTOM23-45"
    "exh-perf-test-AUTOM23-33"
    "exh-perf-test-AUTOM23-40"
    "exh-perf-test-AUTOM23-38"
    "exh-perf-test-AUTOM23-27"
    "exh-perf-test-AUTOM23-43"
    "exh-perf-test-AUTOM23-39"
    "exh-perf-test-AUTOM23-46"
    "exh-perf-test-AUTOM23-47"
    "exh-perf-test-AUTOM23-26"
    "exh-perf-test-AUTOM23-36"
    "exh-perf-test-AUTOM23-48"
    "exh-perf-test-AUTOM23-25"
    "exh-perf-test-AUTOM23-31"
)

# Function to make the curl request
make_request() {
    local exhibitor_id=$1
    local url="${BASE_URL}/${exhibitor_id}?${QUERY_PARAM}"
    echo "Making request to: $url" # Optional: for debugging
    curl -s -o /dev/null -w "%{http_code} %{time_total}s %{url_effective}\n" "$url"
}

export -f make_request # Export the function for GNU Parallel

# Number of concurrent jobs (threads)
NUM_CONCURRENT_THREADS=1000

# Loop to hit the endpoint multiple times to reach 1000 requests
# We need to calculate how many times to repeat the list to get 1000 hits
NUM_IDS=${#EXHIBITOR_IDS[@]}
REPETITIONS=$(( (NUM_CONCURRENT_THREADS + NUM_IDS - 1) / NUM_IDS )) # Ceiling division

echo "Starting $NUM_CONCURRENT_THREADS concurrent requests..."

# Generate the list of exhibitor IDs for the required number of repetitions
# and pipe them to GNU Parallel
for ((i=0; i<REPETITIONS; i++)); do
    printf "%s\n" "${EXHIBITOR_IDS[@]}"
done | head -n $NUM_CONCURRENT_THREADS | parallel -j $NUM_CONCURRENT_THREADS make_request