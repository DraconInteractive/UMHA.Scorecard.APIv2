# ScorecardAPI

Tournament scoring API for the purposes of use by Ursa Major Hema Academy. 

Consists of CRUD operations for Users, Tournaments, Matches and MatchEvents.

<h2>Users</h2> 
Basic CRUD operations
Tracks user first and last name, and what tournaments they're in. 

<h2>Tournaments</h2>
Consists of a list of fighters and matches. 
When creating a tournament, you can specify a generation method and a list of fighters to autopopulate the tournament matchups. 
You can also specify an amount of pools, for larger tournaments.

<h2>Matches</h2>
Matches track 
- two fighters (id + health), 
- duration, 
- start/end time, 
- match status (not started, finished, etc), 
- match pool, and 
- MatchEvents.
Setting match status will automatically update start/end time, and registering a MatchEvent with applyHealthReduction will update the fighter remaining health points.

<h2>Match Events</h2>
Key events from a match including 
- Strikes, 
- Penalties, and 
- Disqualifications. 
Can optionally affect fighter remaining health. 
Disqualifications are tracked on tournament level.
