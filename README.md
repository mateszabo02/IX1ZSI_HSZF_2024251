# Real estate management

Készítsen programot, amely eladásra váró és kiadó ingatlanok adatait, valamint a tulajdonosok és érdeklődők adatait tárolja. A program bemenetére példa az alábbi xml file. Az xml file alapján készítse el az adatbázis sémát es a táblák közötti kapcsolatokat. A program üres adatbázissal is legyen elindítható, támogassa a manuális feltöltést és az xml-ből való beolvasást is.

Minden ingatlanról eltároljuk az alapvető paramétereit. Egy ingatlan akkor kiadó, ha szerepel nála kiadási ár, és akkor eladó, ha rendelkezik eladási árral. Minden ingatlan lehet egyszerre kiadó és eladó is, kiadott ingatlan viszont nem adható el a bérleti szerződés lejárta előtt. Csak budapesti ingatlanokat kezel a rendszer.

Minden személy rendelkezhet ingatlannal és bérleti viszonnyal is, ezek száma nem korlátozott. Az ügyfelekhez rögzíthetünk ingatlankeresési preferenciákat is.

Amikor új ingatlan vagy új személy kerül a rendszerbe, a program automatikusan lefuttat egy keresést, és minden érdeklődőt esemény elsütésével értesít az azok keresési preferenciáinak megfelelő ingatlanokról. Kiadó ingatlant keresőket csak kiadó ingatlanokról, eladó ingatlant keresőket csak eladó ingatlanokról értesít. Ez a keresés manuálisan is elindítható a programból.

Amennyiben az érdeklődőnek megtetszik egy ingatlan, úgy megvásárolhatja/kibérelheti azt. A szerződéskötés a program felületén manuálisan tehető meg, az ügyfélazonosítók és az ingatlanazonosító megadásával. Bérleti szerződés esetén a szerződés lejártának dátuma is szükséges paraméter (a későbbiekben ennek megléte alapján tudjuk eldönteni, hogy bérleti szerződésről vane szó). Az szerződés megkötése során az ingatlan árai törlődnek, ezzel jelezve, hogy az már nem eladó/kiadó. Vásárlás esetén a tulajdonjog is átruházódik. A vevő/bérlő pedig nem keres tovább ingatlant.

A tulajdonosok szintén a program felületén állíthatják az ingatlanukat eladóra/kiadóra, ügyfélazonosító, ingatlanazonosító, ár megadásával. Ugyanezzel a metódussal lehet frissíteni az ingatlan árát is. Egy további metódussal az ügyfelek keresési preferenciái is módosíthatók.

A program felületén biztosítson lehetőséget az alábbi lekérdezésekre:

- eladó ingatlanok, akár szűrve is, az ingatlan paraméterei alapján

- kiadó ingatlanok, az imént említett szűrési lehetőséggel

- top 10 ingatlan, ami a legtöbb ügyfél preferenciáinak megfelel, az ügyfelek száma szerinti csökkenő sorrendben

- eladó ingatlanok átlagos m2 ára, kerületek szerint

Az adatbázisban tárolt adatok (ingatlanok, ügyfelek, megkötött szerződések) a felhasználó által megadott útvonalra, szintén a felhasználó által megadott nevű .txt fájlokba exportálhatók. 
