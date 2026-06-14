# 💰 Správce osobních financí
### Závěrečný projekt — Programování v jazyce C#

---

## 1. Zadání projektu

Cílem projektu je vytvoření **konzolové aplikace v jazyce C#**, která slouží k jednoduché evidenci osobních příjmů a výdajů.

Aplikace uživateli umožňuje:

- **Zadávat** příjmy a výdaje včetně kategorie a popisu
- **Ukládat** záznamy do textového souboru pro uchování dat mezi spuštěními
- **Prohlížet** historii všech transakcí

Kromě základních funkcí aplikace nabízí také **pokročilé funkce**:

- 📊 Zobrazení **vizuálního grafu útrat** rozdělených podle měsíců (ASCII graf v konzoli)
- 🔍 **Filtrování transakcí** podle zadané kategorie
- 🔐 Speciální **Admin režim** pro ruční zadávání transakcí s vlastním datem (vhodné pro zadávání starší historie)

Aplikace je navržena s důrazem na využití principů **objektově orientovaného programování (OOP)** — konkrétně dědičnosti, zapouzdření a polymorfismu — a pracuje s **dynamickými kolekcemi** (`List<T>`).

---

## 2. Model tříd a jejich vazby

Níže je znázorněna struktura tříd a jejich vzájemné vztahy:

```
Transakce  (abstraktní rodičovská třída)
│
├── Prijem         (dědí z: Transakce)
│
└── Vydaj          (dědí z: Transakce)

SpravceFinanci     (obsahuje kolekci: List<Transakce>)
│
└── [agreguje] ──► Transakce (a její potomky)

SpravceSouboru     (pomocná třída, nezávislá na dědičnosti)
└── pracuje s:  List<Transakce>
```

**Přehled vztahů:**

| Třída | Vztah | Druhá třída |
|---|---|---|
| `Prijem` | dědí z | `Transakce` |
| `Vydaj` | dědí z | `Transakce` |
| `SpravceFinanci` | obsahuje (agregace) | `List<Transakce>` |
| `SpravceSouboru` | pracuje s | `List<Transakce>` |

---

## 3. Struktura aplikace

### `Transakce` *(abstraktní třída)*

Rodičovská třída pro všechny typy transakcí. Definuje společné vlastnosti a virtuální metody, které potomci mohou přepsat.

**Vlastnosti:**
- `Datum` — datum provedení transakce
- `Castka` — finanční částka
- `Kategorie` — kategorie transakce (např. Potraviny, Mzda, …)
- `Popis` — volitelný textový popis

**Metody:**
- `VypisDetail()` *(virtuální)* — vypíše detail transakce do konzole
- `ToCsvString()` *(virtuální)* — vrátí transakci jako řetězec ve formátu CSV pro uložení do souboru

---

### `Prijem` a `Vydaj` *(potomci třídy Transakce)*

Obě třídy **přepisují (override)** zděděné metody a přizpůsobují jejich chování:

- `VypisDetail()` — výpis je **barevně odlišen** v konzoli (příjmy zeleně, výdaje červeně) pro lepší přehlednost
- `ToCsvString()` — vrací řetězec s přidaným identifikátorem typu (`Prijem`/`Vydaj`) pro správné načtení ze souboru

---

### `SpravceFinanci`

Hlavní řídící třída aplikace. Uchovává všechny transakce v **dynamické kolekci** `List<Transakce>` a nabízí metody pro práci s nimi.

**Metody:**
- `PridatTransakci()` — přidá novou transakci do kolekce
- `SpoctiZustatek()` — vypočítá a vrátí aktuální finanční zůstatek (součet příjmů minus výdaje)
- `VypisVsechnyTransakce()` — vypíše celou historii transakcí do konzole
- `VykresliGrafMesicu()` — zobrazí ASCII vizualizaci výdajů rozdělených podle jednotlivých měsíců
- `VypisPodleKategorie(string hledanaKategorie)` — vyfiltruje a vypíše pouze transakce odpovídající zadané kategorii

---

### `SpravceSouboru`

Pomocná třída zajišťující **práci s datovým souborem**. Nemá vazbu na dědičnost — jde o samostatnou servisní třídu.

**Metody:**
- `UlozData(List<Transakce>)` — zapíše všechny transakce z paměti do textového souboru
- `NactiData()` — načte transakce ze souboru a vrátí je jako `List<Transakce>`

---

### `Program`

Vstupní bod aplikace. Obsahuje metodu `Main`, která:

- Vykreslí **ASCII úvodní logo** aplikace
- Spustí **smyčku `while`** obsluhující uživatelské menu
- Zpracovává vstup uživatele a volá příslušné metody třídy `SpravceFinanci`

---

## 4. Práce se soubory

Aplikace využívá **knihovnu `System.IO`** a třídy `StreamReader` a `StreamWriter` pro čtení a zápis dat.

Data jsou ukládána do textového souboru **`data.txt`**, který se nachází ve stejné složce jako spustitelný soubor aplikace.

**Formát souboru — každý řádek představuje jednu transakci:**

```
typ;datum;castka;kategorie;popis
```

*Příklad:*
```
Prijem;2025-03-15;25000;Mzda;Výplata za březen
Vydaj;2025-03-18;350;Potraviny;Nákup v supermarketu
```

**Důležitý technický detail — ukládání data v ISO formátu:**

Datum se do souboru ukládá ve formátu **`yyyy-MM-dd`** (mezinárodní ISO 8601). Toto rozhodnutí je záměrné a předchází chybě `System.FormatException`, ke které by mohlo dojít při načítání souboru na počítačích s **odlišným jazykovým nastavením Windows** (např. `dd.MM.yyyy` vs. `MM/dd/yyyy`). Díky pevnému formátu je načítání dat spolehlivé bez ohledu na regionální nastavení systému.

**Životní cyklus souboru:**

- 🟢 **Při spuštění aplikace** — data jsou automaticky načtena ze souboru `data.txt` do paměti
- 🔴 **Při volbě „Uložit a ukončit"** — celý soubor je přepsán aktuálními daty z paměti (přístup *write-all*)

---

## 5. Ovládání aplikace

Program se ovládá zadáváním **číselných voleb v konzolovém menu** a potvrzením klávesou **Enter**. Žádná myš ani grafické rozhraní nejsou potřeba.

Po spuštění aplikace se zobrazí hlavní menu s následujícími možnostmi:

| Volba | Akce |
|:---:|---|
| `1` | ➕ Přidat příjem |
| `2` | ➖ Přidat výdaj |
| `3` | 📋 Vypsat všechny transakce |
| `4` | 📊 Zobrazit měsíční graf výdajů |
| `5` | 🔐 **ADMIN:** Přidat transakci s vlastním (ručně zadaným) datem |
| `6` | 🔍 Hledat transakce podle kategorie |
| `7` | 💾 Uložit data a ukončit program |

> **Poznámka k volbě 5 (Admin režim):** Standardně se při přidávání transakce datum doplní automaticky (dnešní datum). Admin režim umožňuje zadat libovolné datum ručně — je určen pro zpětné doplňování starší finanční historie.

---

*Projekt byl vytvořen jako závěrečná práce v rámci kurzu Programování v jazyce C#.*
