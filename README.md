# RollTheDie

Small game to roll the twelve-sided die with extentions in editor mode.

The game allows a user to throw the 12-sided cube (die) onto a table and count the 
result of the throws.

![game](https://github.com/SvetlanaArt/RollTheDie/assets/148551551/340de3d4-107a-46b2-ae82-84fd368601c0)

## Ver

Unity editor ver 2022.3.19f1

Assets:

- Text Mesh Pro 3.0.6
- DOTween (HOTween V2)  1.2.765
- UniTask 2.5.3

## Class Structure

![Sheme](https://github.com/SvetlanaArt/RollTheDie/assets/148551551/fd940b5b-9d75-44da-89f5-1c6fa932b3d8)

### Two extension scripts:

 **ReadOnlyText**
 
 Create context menu for TextMeshPro. Menu item "Set Readonly" makes component 
readonly. Menu item "Set Editable" makes component editable

 **Vector3Ext**
 
 Add extension method RoundTo to Vector3 that Round Vector3 to the specified 
number of decimal places.

 ## Generate Sides Guide (in Editor mode)
 
 Script Generator attached to GameObject **Side Generator** (Child of GameObject 
**TwelveSidedDie**):

<p align="center">
  <img src="https://github.com/SvetlanaArt/RollTheDie/assets/148551551/8bb014d0-500d-4648-8c77-d8f36f0aba72" width="350">
</p>

Button **“Regenerate Sides”** allow to generate new sides with given prefab 
**“prefabSide”** and put values into ScriptableObject **ValuesUpdate**.

Check **New Values Generate** if you want to generate
 sides with new values from 1 to count of sides (is
 defined in the script by the number of MeshCollider
 sides).
 
 Open in inspector generated **ValuesUpdate** object to
 edit side values. They will be automatically applied to
 the Die sides. You can apply one **ValuesUpdate** to several dies or
 create several objects to test your game.
 
<p align="center">
  <img src="https://github.com/SvetlanaArt/RollTheDie/assets/148551551/0f3f45c6-245a-4add-a53a-6ad8751ed169" width="350">
</p>


