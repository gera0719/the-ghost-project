# The Ghost Project - szkeleton (v0.1)

## Projekt Leírása
A **The Ghost Project** egy 2D-s lopakodós akciójáték, ahol a játékos egy meghiúsult kísérleti projekt alanyának szerepében menekül az elzárt állomásról. A küldetés célja kritikus adatok kinyerése a terminálok feltörésével, miközben el kell kerülni a járőröző biztonsági drónokat és a környezeti veszélyforrásokat.

Ez a verzió a projekt **szkeleton fázisa**, amely az alapvető technikai rendszereket (mozgás, interakció, adatvezérelt pályabetöltés) demonstrálja.

---

## Játékmenet és Irányítás
* **Mozgás:** `A`, `S`, `D`, `Space` vagy Nyilak.
* **Interakció:** `E` billentyű (Terminálok közelében).
* **Cél:** A szektorban található terminálok megkeresése és sikeres feltörése (puzzle), majd a kijárat elérése.
* **Akadályok:** A pályán elhelyezett veszélyes zónák, melyek érintése azonnali kudarcot jelent.

---

## Jelenlegi Állapot és Fejlesztési Megjegyzések

### 1. Vizuális Megjelenítés (Assetek)
A projekt jelenleg kezdetleges fejlesztési fázisban van, így a **játékos karakter, a drónok és a hazardok** helyett ideiglenes **placeholder assetek** szerepelnek a játékban. Ezeket a végleges, egyedi grafikai elemek elkészülte után cseréljük le.

### 2. Pályák és Adatszerkezet
Jelenleg az első pálya (Quarantine Zone) érhető el. A második és harmadik pálya háttereinek kidolgozása folyamatban van, így a világot tartalmazó JSON fájl is egyelőre csak az első szektor adatait és konfigurációit tartalmazza.

### 3. Terminál Puzzle Rendszer
A terminálokhoz érve az interakció elindítja a feltörési folyamatot. Az első típusú puzzle (kábelösszekötős mini-game) logikailag elkészült egy Canvas alapú felületre, azonban a vizuális megjelenítéssel jelenleg technikai problémák adódtak. Ez a hiba orvoslás alatt áll, a logika a háttérben már működik.

### 4. Felhasználói Felület (UI) és Narratíva
* A végleges grafikus UI felület (HUD, menük, párbeszédpanelek) elkészítése a következő fázis feladata.
* **Történet:** Az első szektorok háttértörténete és a párbeszédek a JSON fájlban már rögzítve vannak. A UI hiánya miatt ezek a szövegek jelenleg a **Debug Log**-ba (Console) kerülnek kiírásra az interakciók során. A grafikus felület befejezése után ezek természetesen a játékos képernyőjén fognak megjelenni.

### 5. Buildelés
A játkhoz egyelőre Windows platformra készült build profil. Build után .exe formátumban futtatható a fájl.

---

## Technikai Háttér
* **Adatvezérelt kialakítás:** A pálya minden eleme (kezdőpont, ellenfelek helyzete, terminálok adatai és a történet) a `StreamingAssets/Levels/level_01.json` fájlból töltődik be.
* **Manager-alapú architektúra:** A játékmenet logikáját és a globális állapotokat a `GameManager` (Singleton) kezeli, biztosítva a stabil működést.

---
*Készült a Ghost Project fejlesztői csapata által.*