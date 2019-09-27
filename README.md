# Labb-4
Er uppgift är att skriva ett program i form av ett spel, som använder objektorienterade principer.
Laborationen ska utföras i grupper om två.
Var gärna kreativa när ni gör laborationen - men fråga Pontus först!
Spelet
Målet med spelet är att hitta till utgången på så få drag som möjligt. Varje drag kan spelaren välja en riktning att gå. Varje förflyttning kostar ett drag.

Spelet utspelar sig på en tvådimensionell karta. Varje ruta på kartan är antingen ett rum, en vägg, en dörr eller utgången. Spelaren kan gå till angränsade rum horisontellt eller vertikalt. Dörrarna är ursprungligen låsta, men genom att besöka olika rum kan man plocka upp nycklar som låser upp dörrar.

Exempel på en karta. Använd en tvådimensionell array.
##############   n: rum med nyckel,  U: utgång (exit),  D: dörr
#.n...#U.....#   M: rum med monster
#...@.####D###   @: spelarens position
#nM...D.D...k#
##############

Förutom kartan behöver spelet skriva ut vilka nycklar eller andra föremål man har plockat på sig och hur många drag man använt.

Använd en array för att representera kartan.
Rutor (rum, dörrar och väggar) ska vara klasser som har en gemensam, abstrakt basklass. Gemensamt för varje ruta är att det har ett tecken som ska visas när man skriver ut kartan på konsolen.
Ett rum kan vara tomt, ha en eller flera nycklar samt ha monster. Om spelaren går på en ruta med ett monster så kostar det många extra drag. Ni kan använda en klass för nycklar eller låta dem vara egenskaper i en klass. Nycklarna är engångsnycklar, som förstörs efter att man använt dem.
Använd en enum för att representera olika sorters rutor.
Använd ett interface för alla klasser som har ett tecken som kan visas på kartan.
Tips & FAQ
Använd W,A,S,D för förflyttningskommandon. Gör så att nycklar används automatiskt.
Börja med att fundera ut vilka klasser ni behöver och de viktigaste egenskaperna innan ni börjar koda!
Skapa ett GitHub-repo med Visual Studio GitHub extension innan ni skapar ett projekt
Gör kartan rektangulär
Inlämning via ithsdistans
Uppgiften lämnas in på ithsdistans
Eftersom det är en gruppuppgift så lämnar en partner in koden
Ladda upp hela projektet som en kommentar, skriv github länken i ett fritextfält. 
Den partner som inte lämnar in kod skall istället lämna in en kommentar. Till exempel
“Jobbar i grupp med Pontus Lindgren”

Användbara funktioner
Console.ReadKey  (använd för att ta emot kommandon från användaren, t.ex. WASD för förflyttningar)
Console.Clear
Console.BackgroundColor, Console.ForegroundColor, Console.ResetColor 

Bedömningskriterier
För godkänt krävs
det ska finnas en klasshierarki: basklasser och subklasser
ni ska använda privata egenskaper och get/set-properties
ni ska använda minst ett interface och minst en abstrakt klass
varje klass ska vara i en egen fil
en klass som representerar spelaren
klasser för olika sorters rutor och föremål
det ska gå att klara spelet och se sin poäng
git:
båda personerna som gjort labben ska vara GitHub-admins (Settings → Collaborators för repot)
båda ska ha gjort minst en commit var till repot
minst 5 commits totalt
minst tre personer ska ha spelat kartan och deras highscore ska stå som en kommentar i koden
ni använder kommentarer i koden för att förklara där den behöver göras begriplig

För väl godkänt krävs
informativa commit-meddelanden
kartan börjar blank, man kan bara se de yttersta väggarna och rutor som angränsar till spelaren horisontellt eller diagonalt
supernycklar, välj minst en av:
nycklarna kan ha olika färg, som bara passar till en specifik dörr
vissa nycklar är flergångsnycklar, som går att använda fler än 1 gång, men inte obegränsat många gånger
minst 3 av följande:
vissa hinder kan deaktiveras med knappar som finns i vissa rum - en ny handling
rum kan ha fällor (synliga eller osynliga) som orsakar extra drag att ta sig över
fler föremål, som kan användas på dörrar, monster eller fällor; eller som ger poäng
mer "realistiska" strider med hit points
rum som man inte ser vad de innehåller förrän man går in i dem
Lämna in innan deadline
Påvisa en god förmåga att kunna hitta en balans mellan att:
 - Undvika överflödig kod så som i redundant kod, dubbletter och stycken som kan ersättas med loopar eller uttryck med mera
 - Kommentera eller namnge komplicerade uttryck och stycken
 - Undvika komplicerade uttryck och stycken i fördel för fler rader kod med bättre läsvänlighet
Väl namngivna identifiers så att syftet av användandet blir uppenbart
Till den mån det är möjligt alltid använda passande datatyper till värden
Uniform indentering och kodstil. Se "Coding convention"
Om elev använder delar utav C# språket som inte specificeras i kriterier eller beskrivningen på uppgiften. Så skall eleven kunna motivera varför detta gjordes, med ovanstående VG kriterier som argument.
.
