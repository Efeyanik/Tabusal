<div align="center">
  <img src="https://github.com/Efeyanik/Tabusal/blob/main/NEWLOGO.jpg" alt="Burn Art Games Logo" width="150"/>

  # TABUSAL
  **A Neo-Noir Mobile Party Game Experience**

  [![Unity](https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white)](#)
  [![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](#)
  [![Android](https://img.shields.io/badge/Android-3DDC84?style=for-the-badge&logo=android&logoColor=white)](#)
</div>

---

## 📌 About The Project

**TABUSAL** is a fast-paced, competitive mobile word game designed with a dark vintage, neo-noir aesthetic. Moving away from the bright and casual looks of traditional party games, it offers a premium, tension-driven experience—especially in its signature "Bomb Mode."

Developed entirely in **Unity / C#**, this repository showcases the core architectural systems, scalable UI/UX designs, and dynamic data management structures built for the game.

## ✨ Core Features

* **Dynamic Localization Engine:** A custom-built, scalable localization system that fetches vocabulary and UI texts from encrypted JSON structures, currently supporting English, Turkish, Spanish, and French seamlessly.
* **Adrenaline-Driven "Bomb Mode":** A custom game loop that introduces randomized tension, forcing players to explain words against an unpredictable, ticking countdown.
* **Premium UI/UX System:** Implemented precise slider mechanics (snapping features) and custom-styled retro components for an immersive, tactile mobile experience.

---

## 🛠️ Technical Architecture & Core Systems
This project was engineered with a modular, manager-based architecture — a pattern commonly used in production-grade Unity titles. Every major system is decoupled into its own manager, communicating through clean data pipelines rather than direct dependencies.
### 1. LocalizationManager.cs & LanguageToggler.cs & LocalizedText.cs
A fully custom-built, runtime localization pipeline supporting English, Turkish, Spanish, and French. Rather than relying on third-party localization packages, the system was architected from scratch: LocalizationManager parses encrypted JSON card data per language, LocalizedText components automatically bind UI elements to the active language, and LanguageToggler handles seamless in-game language switching — all without scene reloads.
### 2. GameManager.cs & BombModeGameManager.cs
Two independent game loop controllers built as state machines — one for Standard Mode, one for Bomb Mode. They handle team turn logic, score tracking, round transitions, and timer management while keeping the UI thread perfectly in sync. The separation ensures neither mode bleeds logic into the other, making future game mode additions trivial.
### 3. UIManager.cs & BombUIManager.cs
A layered UI management system that orchestrates panel transitions, animations, and real-time state feedback across both game modes. Built to handle complex multi-panel flows (main menu → pre-round → gameplay → score screen) without scene switching, keeping load times instant.
### 4. DataManager.cs & GameData.cs & GameSettings.cs
A persistent data pipeline that separates runtime game state (GameData) from user preferences (GameSettings), both managed and serialized through DataManager. This ensures settings like target score, round time, and language preference survive across sessions cleanly.
### 5. PreRoundManager.cs & PauseManager.cs
Lightweight but critical flow controllers. PreRoundManager handles the team handoff sequence between rounds (preventing card peeking), while PauseManager safely suspends and resumes game state — including async timers — without corruption.
### 6. CardPanelSwip.cs & CardIdleAnimation.cs
Custom touch input and animation controllers built specifically for mobile. CardPanelSwip implements swipe gesture detection for card navigation, while CardIdleAnimation adds subtle idle motion to cards — both contributing to the tactile, premium feel of the UI.


## 📸 Screenshots & UI Design

<div align="center">
  <img src="https://github.com/Efeyanik/Tabusal/blob/main/En_StdMode.jpg" width="250"/>
  <img src="https://github.com/Efeyanik/Tabusal/blob/main/En_bombmode.jpg" width="250"/>
  <img src="https://github.com/Efeyanik/Tabusal/blob/main/En_Settings.jpg" width="250"/>
</div>

---

## 🚀 Play The Game

TABUSAL is currently in the Closed Testing phase on the Google Play Console, preparing for its V1.0 Release.

Note: Card content files are not included in this repository.




