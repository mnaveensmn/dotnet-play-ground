using csharp_foundation.Excercise;

// MaxNumber maxNumber = new();
// maxNumber.Execute();

// DiceGame diceGame = new();
// diceGame.Play();

// var SubscriptionRenewal = new SubscriptionRenewal();
// SubscriptionRenewal.Notify();

// var fraudulentOrder = new FraudulentOrder();
// fraudulentOrder.Find();

// var warehouseInventory = new WarehouseInventory();
// warehouseInventory.Validate();

// BooleanExpression booleanExpression = new BooleanExpression();
// booleanExpression.Execute();

//new ConditionalOperator();

//new CoinFlip();

//new RoleBasedPermission();


//new CodeBlockVariableScope();

var timeProvider = TimeProvider.System;
var expirtyTime = timeProvider.GetUtcNow().AddHours(1).DateTime;
Console.WriteLine(DateTime.UtcNow.ToString());
