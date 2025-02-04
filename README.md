***2D Farming Simulator Game Development***

This project focuses on the development of a 2D farming simulator game using Unity, C#, and Visual Studio. It includes a deep market research of video games and modern development technologies, which helped in understanding player expectations and selecting optimal tools for game mechanics.

The project involved detailed game design, covering gameplay mechanics, interaction systems, and economic models. 
Key systems developed include:

- Inventory System: A functional inventory for managing resources and items.
- Plant Growth System: Mechanics for planting, watering, and harvesting, with various growth stages and care requirements.
- Time System: A dynamic day-night cycle, seasonal and yearly changes, influencing gameplay.
- Economic System: A basic model for selling crops, financial management, and dynamic pricing, adding strategy and resource management.
- UI: An intuitive interface for managing inventory, plant status, and finances.

_____
**Main Scripts**
_____

The CharacterController2D script, which is responsible for handling the behavior of a 2D character in the game. The script allows the character to perform a range of actions, such as movement, interacting with objects, using tools, and managing crops. This script is integral to the gameplay mechanics, as it defines how the player can interact with the game world. Let’s dive deeper into how it works and the specific functions it performs.

<p>Key Variables and Components</p>

The script makes use of several variables that handle different aspects of the character’s behavior:


- Movement speed and physics components (like Rigidbody2D) manage the character's movement.
- Vectors store the direction of current and previous movements.
- The object that holds the visualized items, the item renderer, and the inventory component are responsible for managing the character’s inventory and interactions with items.
- The interaction zone defines the area where the character can interact with objects.
- The animation component handles the character’s animations, with boolean variables storing movement and item holding states.

<p>Game Initialization and Continuous Update</p>

At the start of the game, the components are initialized. The script then continuously updates every frame. The script reads user input from the arrow keys or WASD for movement, and the spacebar for actions. It calculates the movement direction, applies speed to move the character, and updates the character’s animations based on the direction and movement state. It also checks whether the character is holding an item, updating the corresponding item animation and render. When the spacebar is pressed, the script executes specific actions like using tools or planting crops.

_____

The **Inventory class** implements a wide range of functions for managing the in-game inventory, including adding, removing, moving, and checking items in the slots. This class makes it easy to manage the inventory and its items within the game. It involves properties and methods responsible for managing the character’s inventory. The inventory size and the list of slots are stored in properties, along with the index of the active slot. Methods for adding, removing, clearing, and checking items in the inventory ensure its functionality. Additionally, there are methods for managing slots, such as searching, activating, and exchanging items between them. Functions are also provided to check for the presence of tools, plants, and harvests in the inventory slots.

The **InventorySlot script** represents an inventory slot that can store an ItemStack and track its state changes. It enables the activation or deactivation of the slot and sends notifications about state changes via events.

The **GameItem script** manages in-game items that can be picked up and thrown within the game world. This script includes functions that handle various aspects of object behavior. Components are initialized when the script is loaded, while the object setup and coroutine initiation to enable the collider are executed at the start. Calling the object setup method when inspector values change ensures the parameters are up to date. Setting up the sprite, item count, and object name is done through separate methods, allowing the object to adapt to new conditions.

A function for removing objects from the game and returning their stack implements the item pickup mechanism, while another method is responsible for throwing the item with a given force. Coroutines that disable gravity after throwing and manage the collider’s state ensure the object interacts physically with the environment. An additional function allows setting a new item stack for the object.

The **ItemStack class** stores information about the quantity of units, helping manage the number of items in the inventory. During harvest initialization, it randomly determines the rarity of the harvest.

The **HarvestDefinition subclass** defines harvests with unique characteristics such as rarity and condition. It also specifies the number of days after harvesting, indicating how long the harvest can be stored.

The **PlantDefinition subclass** adds definitions for plants that can be grown in the game. It includes additional attributes such as the number of days for growth, the item to be harvested, sprites for different growth stages, the number of possible harvests, and the number of items gathered. This approach allows the definition and management of various types of plants in the game, including their growth and harvest cycles.

The **ToolsDefinition subclass** extends the base ItemDefinition class, adding definitions for tools that can be used in the game. Tools are categorized by type, such as hoe, watering can, axe, and pickaxe. This categorization allows for defining different tools and using them accordingly in the game.

Finally, the **ItemDefinition class** defines the basic properties of items in the game’s inventory. It allows the creation of items with attributes like name, stackability, game sprite, icon, and description. This class provides flexibility and easy customization, facilitating the creation of a wide range of items without modifying the main game code.
