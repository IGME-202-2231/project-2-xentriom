# Reef Rendezvous

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

### Student Info

-   Name: Jason Chen
-   Section: 05

## Simulation Design

- An tropical sea of marine life

### Controls

-   Ideally, I want the player to have no control but for the requirement
-   You can drop small fish food in and the smaller fish will try and get it.

## Small Fish

The smaller fish will flock together and try to stay near each other. If the group is too large they will split into smaller groups. They will also attempt to avoid the bigger fishes.

### Flock

**Objective:** Will seek and join a group.

#### Steering Behaviors

- Flee - Big Fish
- Flock - joins together up to 10 (exact number may change)
- Seperation - If comes in contact with big fish, everyone will scatter OR if the group is too big they will split into 2 smaller ones
   
#### State Transitions

- You can only be in this state if you are not already in a group
   
### Wander

**Objective:** The fish will wander around and attempt to join groups within its detection range. It will also have a much bigger flee range to flee to big fishes. They will also seek fish food dropped by the player.

#### Steering Behaviors

- Wander - Self explanitory
- Flee - Bigger range from other state and flees from big shark
- Seek - Seek food
   
#### State Transistions

If seperated from group and alone, you will be in this state

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

-   Background: https://olgas-lab.itch.io/underwater-background
-   Marine Life: https://rkuhlf-assets.itch.io/aquatic-animal-models
-   Marine Life: https://pixel-duarte.itch.io/pixel-ocean


## Make it Your Own

- New Agent: Octopus/Squid - Neutral mob that just bobs up and down, has a 5% chance to wander for 5 seconds (Everything will avoid the Jellyfish)
- Soon to be more

## Known Issues

_List any errors, lack of error checking, or specific information that I need to know to run your program_

### Requirements not completed

_If you did not complete a project requirement, notate that here_

