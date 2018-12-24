# LineupUsageEstimator
User Interface to help generate Strat-O-Matic Baseball lineups based on usage.

**About this application**
The Strat-O-Matic Computer Baseball games allows you to create multiple lineups based on the pitchers arm and the balance of the pitcher.  Many leagues have strict usage requirments for the players.  In a autoplay league it can be hard to estimate how much usage a player will get based on the lineups.

This application will review opponent teams and take into account the schedule balance to come up with an estimate of how many at bats a player will get with a given lineup.

There are multiple calculators that can be used to estimate the at bats

| Calculator | Description |
| --- | --- |
| Weighted Full Roster | (Default) This calculator will review each team and rank the top 5-7 starting pitchers (based on IP/WHIP), the top 5 relief pitchers and the top closer.  It is estimated starting pitchers will pitch 6IP, 2 for relief and 1 for closer.  The schedule balance is taken into account to adjust how often you may face a single team. |
| Weighted Starter Only  | This calculator will only estimate based on all team Starting Pitchers but the schedule balance is taken into account, this is good if starting pitchers tend to go long |
| Starter Only | This is the original calculator and is only Starting Pitchers numbers despite ranking.  This is fine for balanced schedules, or if there is a problem with another calculator |

**Installation**


**Before you start**
*STEP ONE* 
The program uses the SOM Baseball game roster report to populate data and looks for it at the location the application is running from.  

To create the file:
1. Select any team in your league
2. From the `Team` menu select `Display Reports`
3. Select the `Roster Report`
4. Select `Each Team`
5. Save the file to the above location by Selecting `Print to File` from the `File` menu.
6.  Make sure you name the file rosterreport.PRT"

*STEP TWO*
Lookup or have available the three letter team codes assigned to each team.

*STEP THREE*
Understand your league schedule.  The application will need to know the team division breakout and the games played in and out of division.

**Running the First Time**
The application will launch and begin parsing the report file to collect data.

*TODO*:   Team.cs file needs a dialog to implement this function: prettyTeamName()

*Setting Up Your League*
The first time you run you will be asked to assign each team to a division and specify the number of games played in and out of division.  
  1. Enter the number of games played in and out of division
  2. Next start assigning teams to a divions.  The division names are `A`,`B`,`C`,etc.  The actual names are not important, this is just to identify which teams are in and out of division for any given team.
  3. Once all teams are assigned, the number of games should match what you expect and the SAVE button should be enabled.
  4. Hit save!

*Looking around the Main Screen*
The screen you will be looking at is pretty complicated.  Here is a simple tour;
1. **Primary Team Selection Box** - This has a list of all the teams.  This is how you select the team you would like to manage or view.
2. **Manage Lineup Button** - This is where lineup structures are created.  More on that later
3. **Update Opponents Button** - This will return you back to the screen that assigns teams to divisions
4. **Settings Button** - Some advanced configuration options
5. **Large Blue Area** - This will contain a list of lineups soon
6. **Estimated Batter At Bats**  - This table shows Estimated at bats a hitter will get when added to a lineup that includes this balance rating.

| KEY | DESCRIPTION |
| --- | --- |
| BAL | The Strat-O-Matic Pitcher balance rating.  |
| LH IP | The Number of IP by Lefty pitchers rated at this Balance|
| Est. LH AB | The Estimated number of at bats a hitter will have when assigned to a lineup with this balance rating |
| RH IP | The Number of IP by Righty pitchers rated at this Balance|
| Est. RH AB | The Estimated number of at bats a hitter will have when assigned to a lineup with this balance rating |

7. **Team Batting Information** - Based on the lineups created, this area will calculate how many at bats a hitter has remaing to avoid over uages.

| KEY | DESCRIPTION |
| --- | --- |
| Player | The name of the hitter on the team |
| Prj AB | The number of At Bats estimated over a full season based on the configured lineups |
| Act AB | The number of At Bats the hitter has on their card.  A configurable number is added to this as a buffer |
| Remain | The number of At Bats remaining before hitter estimate goes over limit of at bats |
| Bal | The hitters balance, just for reference |
| Positions | As a hitter is added to a lineup, this area will track what position(s) the player is assigned to|

8. **EXIT** - This will exit the program and save any work done.

**Creating your first Lineup**

1. Select the "Primary Team" from the selection box that you want to manage
2. Hit the *Manage Lineup...* button
3. An Empty Lineup List Dialog will appear.  This dialog is where you will create, edit and reorder lineups.  First we will need to define lineups.
4. Hit the "Add" button to create a lineup
5. This will open the "Lineup Manage Dialog" - This is where you define a lineup.  Select the pitcher arm and the balance rating.  To start, create the common two fulltime lineups (Lefty 9L-9R and a Righty 9L-9R)
6. First Lineup: L 9L-9R
7. Select "Left Handed" pitcher arm and From: 9L and To: 9R
8. Then hit Save, and the lineup will be added to the previous dialog
9. Hit Add Agent to add the Righty Lineup
10. Select "Right Handed" pitcher arm and From: 9L and To: 9R and hit Save
11. If you hit the "Apply template for all teams" Then the lineup you created will be added to all teams.  Save that for more complicated template lineups.
12. Hit the Save button to return to the main screen

**Setting the lineup**
1. The blue area of the main screen should now have both your lineups in.  
2. The first column is a list of all positions.  If you do not have a DH, just leave the value blank.
3. The other columns each represent a lineup that was just created.
4. Each position should have the text "NOT SET" across the grid. 
5. Select a lineup and a position and select a box with the mouse
6. The drop down will list all players who can play that position. SOM Outfielder playing out of position rules apply as long as adjusted range rating is 4 or less.
7. The drop down will contain the player Name, Defense Rating with arm, Balance and At Bats on the player card
8. As positions are filled the *Team Batting Information* section will be populated.





Additional Dependency Projects
https://github.com/bobterhune3/somReporter   (Contains Unit tests)
https://github.com/bobterhune3/somReportUtils (Common shared objects)
