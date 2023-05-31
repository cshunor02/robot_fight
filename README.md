# robot_fight
In Software Technology, we developed an object-oriented graphical application (game) in a team of 4 people. Meanwhile we learned to use GitLab, version controlling, unit testing, apply CI/CD and the standard coding conventions. 

# A <i>Robot Fight</i> játék technikai leírása

## A projekt elérése

A játék repository-ja az alábbi webhelyen található, melyhez INF.ELTE.HU-s azonosítóval kell bejelentkezni.
- [Robot Fight repository](https://szofttech.inf.elte.hu/szofttech-ab-2023/group-03/akacmez/-/)

A játékot a következő paranccsal lehet letölteni:
```
git clone https://szofttech.inf.elte.hu/szofttech-ab-2023/group-03/akacmez.git
```

## Alkalmazás indítása

A <i><b>robot_fight.exe</b></i> fájlra kattintva indítható a játék.<br>
Indítás után megjelenik a főmenü, ahol 3 gomb közül választhatunk:

| Gomb neve    | Leírás                                                                                                                                       |
|--------------|----------------------------------------------------------------------------------------------------------------------------------------------|
| Játék        | Ekkor a játszható módban működik az alkalmazás. Ilyenkor betöltődik a játék beállítására szolgáló felület, ahonnan indítható az adott meccs. |
| Nézői mód    | Ekkor a játék nézői módban működik, tehát nem lehetséges interaktálni, kizárólag nyomon követni az eseményeket.                              |
| Játékszabály | Ekkor betöltödik a játék leírását és szabályait tartalmazó felület.                                                                          |

A továbbiakban a <i><b>Játék</b></i> gomb kiválasztása utáni folyamatokról olvashat.

## Játék Beállításának felülete

Ezt a felületet a főmenüben a <i><b>Játék</b></i> gomb kiválasztása után érhetjük el.<br>
A következő beállításokat végezhetjük el:

| Beállítás címkéje         | Leírás                                                                                                                                                                                                                                 |
|---------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Játékos látótere          | Itt állítható be a játékos látótere. A látótér megadása a játék pályáját felosztó<br>négyzetekben értendő egész szám, mely minden irányban azonos kiterjedésű.                                                                         |
| Tábla szélessége          | Itt állítható be az adott játék táblájának (pályájának) szélessége. A szélesség megadása<br>a játék pályáját felosztó négyzetekben értendő egész szám.                                                                                 |
| Tábla hossza              | Itt állítható be az adott játék táblájának (pályájának) hosszúsága. A hosszúság megadása<br>a játék pályáját felosztó négyzetekben értendő egész szám.                                                                                 |
| Csapatok száma            | Itt állítható be az adott játékban résztvevő csapatok száma.                                                                                                                                                                           |
| Feladatok értéke          | Itt lehet megadni a játék folyamán teljesítendő feladatok minimális és maximális értékét,<br>egész számban.                                                                                                                            |
| Akadályok száma           | Itt állítható be a játék folyamán, a pályán előforduló akadályok számát.                                                                                                                                                               |
| Elemek takarítása (akció) | Itt lehet megadni, hogy egy adott akadályt, vagy dobozt mennyi takarítási interakcióból<br>lehet tényleges eltakarítani.                                                                                                               |
| Kör hossza (másodperc)    | Itt állítható be a játék köreinek hossza másodpercben,<br>mely azonos minden csapat és játékos számára.                                                                                                                                |
| Játék hossza (lépés)      | Itt állítható be a játék teljes hossza lépésekben. Egy lépés az egy körnek felel meg,<br>akár történt interakció, akár nem, a lépésszám csökken minden játékosnak a kör végén.                                                         |
| Kijáratok száma           | Itt lehet megadni a pályán található kijáratok számát,<br>melyek mindig azonos hosszúságúak és a pálya szélén helyezkednek el.                                                                                                         |
| Játékosok száma           | Itt lehet megadni az egyes csapatokban lévő játékosok számát.                                                                                                                                                                          |
| Választható témák         | Összesen 3 fajta téma közül választhatunk:<br><ul><li>Normál téma</li><li>Nemzetközi piknik nap</li><li>Karácsonyi téma</li></ul>Ennek a beállításnak a megváltoztatása nem befolyásolja<br>az üzleti logikát, kizárólag a textúrákat. |

A felületen található a <i><b>Játék indítása</b></i> gomb is, mellyel indítható, a megadott beállításokkal a játék.

## CI folymatok

A scriptek a GitLab pipeline-jában érhetőek el az alábbi webhelyen:
- [CI scripts for Robot Fight](https://szofttech.inf.elte.hu/szofttech-ab-2023/group-03/akacmez/-/ci/editor)
