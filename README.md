
# Internship recruitment task: Ship Fleet Management Library
This library is designed to facilitate the management of a fleet of ships, focusing on container ships and tanker ships. The library ensures data integrity through validation checks for ship details, including IMO numbers, dimensions, and coordinates. Library meets all [Task Requirements](TaskRequirements.pdf).

## Key Features

### Ship Management
* Adding ships
* Ship position update

### Container Ship Operations
* Loading and unloading containers

### Tanker Ship Operations
* Refueling and emptying tanks

## Prerequisites
* IDE supporting .NET eg. Visual Studio 2022, Rider
* .NET 8 SDK

## Steps to Run and Tests the library:
**Step 1**: Clone the repository containing the class library and its unit tests to your local machine using Git:

```bash
  git clone https://github.com/Szymongr14/FleetManagement
```

**Step 2**: Open the Solution in IDE

**Step 3:** Build the Solution

**Step 4:**: Run the Unit Tests `FleetManagementAppTest > Run All Tests`

**Step 4:** Create Console App project and link library inside this solution to test library

## My thought process when coding this task
Shipowner can add two types of ship (container and tanker ship), so I naturally thought about creating abstract class Ship with common fields **{Id, name, width, length, position}** and two inheriting classes from Ship class, which are ContainerShip and TankerShip with custom fields and methods specific for their usage.

For storing ships in the fleet I decided to use HashSet data structure because it ensures that all ships in the fleet are unique and provides efficient insertion, removal, and lookup operations. I override **Equals()** and **GetHashCode()** to compare and count hash depending only on ID, knowing it is unique.

For storing ship's current and previous positions I decided to use **LinkedList** because it has inserting in O(1) complexity and naturally keeps inserting order. 

In the Requirements it is said that refueling and emptying tanks should refers to specific tank, so I added ID to them as well. 

I also created custom exceptions classes for this library therefore users will know more precisely context of thrown exception.

I decided to store GPS timestamp and record time in Position record to have more precise control over the data being processed. It will have crucial impact when GPS signal is significantly delayed.

## Asumptions
* From ship we can fully or partially empty tank only with given ID as it is in the requirements 
* For simulating GPS delay I subtract from current time 70ms and store it into GPSTime variable in Position record.
* Every metric variable is in SI unit eg. MassKg
* Ship's name must be at least 2 characters
* Coordinates must be: Latitude <-90, 90>, Longitude <-180, 180>
* Equals method returns true only when comparing ships from the same subclass i.e. ContainerShip and TankerShip with the same ID.