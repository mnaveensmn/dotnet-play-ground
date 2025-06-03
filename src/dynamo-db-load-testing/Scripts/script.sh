#!/bin/bash

# Base URL and groupId
BASE_URL="https://qr-manager-service-perf.css.rxweb-dev.com/v1/qr-profiles"
GROUP_ID="AUTOM23"

# List of shortCodes
shortCodes=(
  "25FA34" "2862LT" "2J3UYY" "4FNMPK" "4W4M94" "4WPTF7" "6DCYV2"
  "7LHSLD" "9BFE4Z" "F9B23L" "JTHS9Y" "K2YCP1" "LFU3RQ" "QXKKCV"
  "PRY47W" "SV6WTA" "TV2ZNC" "VNWQYG" "W8JMMG" "WM4KUN" "WRSXS5"
  "X5XE2H" "YFX4N3" "Z8LEDW"
)

# Loop through each shortCode
for code in "${shortCodes[@]}"; do
  url="${BASE_URL}?groupId=${GROUP_ID}&shortCode=${code}"

  # Make the request
  response=$(curl -s --retry 3 "$url")

  # Extract exhibitorId using jq
  exhibitorId=$(echo "$response" | jq -r '.data.qrContent.exhibitorId // "N/A"')

  echo "{\"exhibitorId\": \"$exhibitorId\"},"
done
