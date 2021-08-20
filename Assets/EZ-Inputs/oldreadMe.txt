how to use:


1.)
go to Window/PackageManager and inport the new Input system  
if the new input system not settet than go to ProjectSettings/Player/OtherSettings/Configuration/ActiveInputHandling 
and set it to Input System Package (new)

2.)
drag the controllerinput prefab in the scene

how to use

ez like the old input system with a if statment

private Update()
{
	
	if(X_ButtonDown(controller count)){}
	if(X_ButtonUp(1)){}
	if(X_ButtonPressed(1)){}

	if(X_LeftStick(1) == Vector2.zero){}

}