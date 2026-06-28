# The Ghost Project - beta (v1.1)

## Projekt Leírása
A **The Ghost Project** egy 2D-s lopakodós akciójáték, ahol a játékos egy meghiúsult kísérleti projekt alanyának szerepében menekül az elzárt állomásról. A küldetés célja kritikus adatok kinyerése a terminálok feltörésével, miközben el kell kerülni a járőröző biztonsági drónokat és a környezeti veszélyforrásokat.

Ez a verzió a projekt **béta fázisa**, amelyben a követelményként felvett funkciók jelentős része működőképes.

---

## Játékmenet és Irányítás
* **Mozgás:** `A`, `S`, `D`, `Space` vagy Nyilak.
* **Interakció:** `E` billentyű (Terminálok közelében).
* **Cél:** A szektorban található terminálok megkeresése és sikeres feltörése (puzzle), majd a kijárat elérése.
* **Akadályok:** A pályán elhelyezett veszélyes zónák, melyek érintése azonnali kudarcot jelent.

---

## Jelenlegi Állapot és Fejlesztési Megjegyzések

### 1. Vizuális Megjelenítés (Assetek)
A projekt jelenlegi fázisában a grafikai elemek közel véglegesnek tekinthetőek. A későbbiekben várható a **játékos karakter** és a **sav tócsa** grafikai finomítása.

### 2. Pályák és Adatszerkezet
Jelenleg az első (Karantén zóna) és második pálya (Zsilip) érhető el. A harmadik pálya grafikai kidolgozása kész, azonban még nem került beépítésre. A világrészleteit tartalmazó JSON fájl egyelőre az első és második szektor adatait és konfigurációit tartalmazza.

### 3. Terminál Puzzle Rendszer
A terminálokhoz érve az interakció elindítja a feltörési folyamatot. Az első típusú puzzle (kábelösszekötős mini-game) elkészült egy Canvas alapú felületre, beépítésre került a játékba. A továbbiakban különféle fejtörők kialakítása a fejlesztési terv része.

### 4. Felhasználói Felület (UI) és Narratíva
* A végleges grafikus UI felület (HUD, menük, párbeszédpanelek) elkészítése a következő fázis feladata.
* **Történet:** Az első és második szektor háttértörténete és a párbeszédek a JSON fájlban már rögzítve vannak. A UI hiánya miatt ezek a szövegek jelenleg a **Debug Log**-on (Console) kerülnek kiírásra az interakciók során. A grafikus felület befejezése után ezek természetesen a játékos képernyőjén fognak megjelenni.

### 5. Fordítási információk

* **Fejlesztőkörnyezet** Unity 6000.4.4f1

#### Fordítás lépései
1. Projekt megnyitása Unity editorban.
2. `_Project\Scenes` mappában található `MainGame` jelenet megnyitása.
3. **File -> Build profiles...** opció kiválasztása a menüből.
4. **Windows** opció kiválasztása
5. A **Build** opció kiválasztásával a Unity legenerálja a .exe fájlt és az szükséges könyvtárakat.

### 6. Futtatási információk
1. A lefordított játékot tartalmazó mappa megnyitása.
2. **GhostProject.exe** fájl elindítása.
3. A játék teljes képernyős módban elindul és betölti az első szektort.

---

## Technikai Háttér
* **Adatvezérelt kialakítás:** A pálya minden eleme (kezdőpont, ellenfelek helyzete, terminálok adatai és a történet) a `StreamingAssets/Levels/level_01.json` fájlból töltődik be.
* **Manager-alapú architektúra:** A játékmenet logikáját és a globális állapotokat a `GameManager` (Singleton) kezeli, biztosítva a stabil működést.

---
*Készült a Ghost Project fejlesztői csapata által.*
