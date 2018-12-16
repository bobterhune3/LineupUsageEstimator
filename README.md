# LineupUsageEstimator
User Interface to help generate Strat-O-Matic Baseball lineups based on usage.

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



Additional Dependency Projects
https://github.com/bobterhune3/somReporter   (Contains Unit tests)
https://github.com/bobterhune3/somReportUtils (Common shared objects)
