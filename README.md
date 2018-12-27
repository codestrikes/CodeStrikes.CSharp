# CodeStrikes (http://codestrikes.net/)

## SDK
This is SDK for C# language. You can use it to write and test Your BOT.

## Code your bot and compete with others.
Codestrikes is entertaining platform designed for software engineering passionates. Enter Codestrikes and become one of the players, 
compete with others by creating best algorithms to lead your bot during fights with opponents.

1. Write code
Write your own algorithm, define strikes and blocks for each round. Think about the strategy for fights and translate it into the 
code and lead your bot to fight against others. You can code using different programming languages: C#, Java, Typescript.

2. Enjoy competition in 3D real time scenario !
Upload your code and enjoy the competition. You will see how your bot fights against others in real time 3D game scenario using your 
own algorithm.

3. Analyze results
Analyze your opponents moves during competition and improve your algorithm to take 1st place. Let your algorithm to learn and analyze 
other fighters attacks and combinations to lead with best attacks and gain most points.

4. Have fun
Codestrikes is a platform which gives you a lot of fun, an opportunity to challenge your code skills and encourages to improve your 
algorithm to be the best one!

## Rules
  
You have **150 HP** and **12** energy points per round. During fight you have to attack your opponent, but also try to defend against his attacks. There is table below, with all attack statistics:  
  

Attack           | Energy points  | Hit points |
-----------------| --------------:|-----------:|
Hook Kick        |              4 |         10 |
Hook Punch       |              3 |          6 |
Uppercut Punch   |              2 |          3 |
Low Kick         |              1 |          1 |

  
You can defend against your opponents attacks using following blocks. One defence type blocks all opponents same type hits.  
  

Defence          | Energy points |
-----------------| -------------:|
Hook Kick        |             4 |
Hook Punch       |             4 |
Uppercut Punch   |             4 |
Low Kick         |             4 |
  
**Example:**  
Your fight combination is:

1.  Hook kick (4 EP)
2.  2 x Low kick (2 EP)
3.  Uppercut punch (2 EP)
4.  Low kick block (4 EP)
  
Result of this combination means, that you will attack with combo of Hook kick, uppercut punch and 2 x low kick. You will be able to defend yourself against all opponent's lowkicks.


**RoundContext**  
  
RoundContext parameter stores information about last opponent moves, damage dealt and damage taken in previous round. This data may be helpfull during coding your fight algorithm.  
  

* Property
* Description
* LastOpponentMoves
* Collection of last opponents moves  
* MyDamage
* Damage dealt to opponent
* OpponentDamage
* Damage taken in last round
* Available methods:
  1.  GetAttacks - returns all opponents attacks
  2.  GetDefences - return all opponents defences
  
  
** Important: **
You are able to collect your opponents moves from all previous rounds for further analysis by declaring variable outside MoveCollection method.

  
**Testing your alghoritm**  
You can test your alghoritm in the isolated application:

1.  Download source codes
2.  Open project in Visual Studio
3.  Paste / write your code in PlayerBot.cs class
4.  Your alghoritm will be tested against to sample bots
