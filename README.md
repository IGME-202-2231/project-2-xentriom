# Project _NAME_

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

_REPLACE OR REMOVE EVERYTING BETWEEN "\_"_

### Student Info

-   Name: Jason Chen
-   Section: 05

## Simulation Design

- An aquarium with different species of marine life. Some species may flock together and some species may avoid each other.

### Controls

-   Ideally, I want the player to have no control but for the requirement
-   You can drop small fish food in and the smaller fish will try and get it.

## Small Fish

The smaller fish will flock together and try to stay near each other. If the group is too large they will split into smaller groups. They will also attempt to avoid the bigger fishes.

### Seek Group

**Objective:** Will seek and join a group.

#### Steering Behaviors

- Flee - Big Fish
- Obstacles - N/A
- Seperation - N/A
   
#### State Transitions

- You can only be in this state if you are not already in a group
   
### _State 2 Name_

**Objective:** _A brief explanation of this state's objective._

#### Steering Behaviors

- _List all behaviors used by this state_
- Obstacles - _List all obstacle types this state avoids_
- Seperation - _List all agents this state seperates from_
   
#### State Transistions

- _List all the ways this agent can transition to this state_

## Big Fish

The bigger fish will avoid each other and for the most part wander around aimlessly.

### Chase

**Objective:** Chase a lonely small fish and "eat" it

#### Steering Behaviors

- Seek - Small Fish
- Obstacles - Other Big Fish
- Seperation - N/A
   
#### State Transistions

- If a small fish is within detection range AND they are alone
   
### Wander

**Objective:** Wander around the world, enjoying life.

#### Steering Behaviors

- Wander - Self explanitory
- Obstacles - Other Big Fish
   
#### State Transistions

-Default state, will change to this state if cannot Chase.

## Sources

-   _List all project sources here –models, textures, sound clips, assets, etc._
-   _If an asset is from the Unity store, include a link to the page and the author’s name_

## Make it Your Own

- New Agent: Jellyfish - Neutral mob that just bobs up and down, has a 5% chance to wander for 5 seconds (Everything will avoid the Jellyfish)
- Soon to be more

## Known Issues

_List any errors, lack of error checking, or specific information that I need to know to run your program_

### Requirements not completed

_If you did not complete a project requirement, notate that here_

