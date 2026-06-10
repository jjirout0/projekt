# 💰 Správce osobních financí
### Závěrečný projekt – Programování v jazyce C#

---

## 1. 📋 Zadání projektu

Cílem projektu je vytvořit **konzolovou aplikaci v jazyce C#**, která slouží k jednoduché evidenci osobních příjmů a výdajů.

Aplikace uživateli umožňuje:
- **zadávat příjmy a výdaje** s kategorií, popisem a datem,
- **ukládat záznamy do souboru** a při dalším spuštění je opět načíst,
- **prohlížet historii** všech zadaných transakcí,
- **zobrazit jednoduchý vizuální graf** přehledu financí podle měsíců.

Projekt demonstruje využití principů **objektově orientovaného programování (OOP)** – konkrétně dědičnosti, polymorfismu a zapouzdření – a práci s **dynamickými kolekcemi** (`List<T>`).

---

## 2. 🗂️ Model tříd a jejich vazby

```
Transakce  (abstraktní třída)
│
├── Prijem        (dědí z Transakce)
└── Vydaj         (dědí z Transakce)

SpravceFinanci    (obsahuje kolekci List<Transakce>)
SpravceSouboru    (pomocná třída – práce se souborem, bez vazby na dědičnost)
```

### Přehled vazeb

| Třída | Typ vazby | Popis |
|---|---|---|
| `Prijem` | dědičnost (`Transakce`) | Rozšiřuje abstraktní třídu o specifický výpis příjmu |
| `Vydaj` | dědičnost (`Transakce`) | Rozšiřuje abstraktní třídu o specifický výpis výdaje |
| `SpravceFinanci` | kompozice | Drží kolekci `List<Transakce>` a pracuje s ní |
| `SpravceSouboru` | samostatná pomocná třída | Načítá a ukládá data, nevztahuje se k dědičnosti |

---

## 3. 🏗️ Struktura aplikace

### `Transakce` *(abstraktní třída)*
Společný základ pro všechny typy transakcí.

**Vlastnosti:**
- `Datum` – datum zadání transakce
- `Castka` – finanční hodnota transakce
- `Kategorie` – kategorie (např. jídlo, bydlení, mzda…)
- `Popis` – volný textový popis

**Metody:**
- `VypisDetail()` – **virtuální** metoda pro výpis detailu transakce do konzole
- `ToCsvString()` – **virtuální** metoda pro převod transakce do textového řetězce ve formátu CSV

---

### `Prijem` a `Vydaj` *(potomci třídy Transakce)*
Konkrétní typy transakcí, které **přepisují (override)** rodičovské metody.

- `VypisDetail()` – barevný výpis do konzole (příjmy zeleně, výdaje červeně)
- `ToCsvString()` – vrací řetězec s typem `"prijem"` nebo `"vydaj"` jako prvním polem

---

### `SpravceFinanci`
Hlavní třída aplikace, která spravuje seznam všech transakcí.

**Kolekce:** `List<Transakce>` – dynamický seznam všech zadaných záznamů

**Metody:**
- `PridatTransakci()` – přidá novou transakci do kolekce
- `SpoctiZustatek()` – vypočítá a vrátí aktuální finanční zůstatek (příjmy − výdaje)
- `VypisVsechnyTransakce()` – vypíše celou historii transakcí přehledně do konzole
- `VykresliGrafMesicu()` – zobrazí jednoduchý vizuální sloupcový graf příjmů a výdajů podle měsíců

---

### `SpravceSouboru`
Pomocná třída zajišťující perzistenci dat.

**Metody:**
- `UlozData(List<Transakce>)` – uloží předanou kolekci transakcí do souboru
- `NactiData()` – načte transakce ze souboru a vrátí je jako `List<Transakce>`

---

### `Program`
Vstupní bod aplikace.

- Obsahuje metodu **`Main()`**, kde běží hlavní smyčka programu (`while` cyklus) s konzolovým menu pro interakci s uživatelem.

---

## 4. 💾 Práce se soubory

Aplikace pro ukládání dat využívá knihovnu **`System.IO`** a třídy **`StreamReader`** a **`StreamWriter`**.

Data jsou uložena v jednoduchém textovém souboru s názvem **`data.txt`**, který se nachází ve složce spustitelného souboru.

### Formát souboru

Každý řádek souboru představuje **jednu transakci**. Hodnoty jsou odděleny středníkem:

```
typ;datum;částka;kategorie;popis
```

**Příklady záznamů:**
```
prijem;2025-03-01;25000;Mzda;Výplata za únor
vydaj;2025-03-05;1200;Jídlo;Nákup v supermarketu
vydaj;2025-03-10;850;Doprava;Měsíční jízdenka
```

### Načítání a ukládání

| Událost | Chování |
|---|---|
| **Spuštění programu** | Soubor `data.txt` se automaticky načte a transakce se vloží do kolekce |
| **Volba „Uložit a ukončit"** | Celý soubor se **přepíše** aktuálním stavem kolekce v paměti |

> ⚠️ **Poznámka:** Data se neukládají průběžně – je nutné program ukončit volbou č. 6, jinak se změny ztratí.

---

## 5. 🎮 Popis ovládání

Program se ovládá zadáváním **čísel 1–6** v konzolovém menu a potvrzením klávesou **Enter**.

Po spuštění aplikace se zobrazí hlavní menu:

```
========================================
       SPRÁVCE OSOBNÍCH FINANCÍ
========================================
  1 - Přidat příjem
  2 - Přidat výdaj
  3 - Vypsat všechny transakce
  4 - Zobrazit měsíční graf
  5 - ADMIN: Přidat transakci s vlastním datem
  6 - Uložit a ukončit program
========================================
Váš výběr:
```

### Popis jednotlivých voleb

| Číslo | Akce | Popis |
|---|---|---|
| **1** | Přidat příjem | Uživatel zadá částku, kategorii a popis; datum se doplní automaticky |
| **2** | Přidat výdaj | Stejný postup jako u příjmu, záznam se uloží jako výdaj |
| **3** | Vypsat transakce | Zobrazí barevný přehled všech transakcí a aktuální zůstatek |
| **4** | Měsíční graf | Vykreslí jednoduchý textový graf příjmů a výdajů po měsících |
| **5** | ADMIN – vlastní datum | Umožňuje zadat transakci se zpětně nastaveným datem (ruční vstup) |
| **6** | Uložit a ukončit | Uloží všechna data do souboru `data.txt` a ukončí program |

---

*Projekt vypracován jako závěrečná práce z předmětu Programování v jazyce C#.*
