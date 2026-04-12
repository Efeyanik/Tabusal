<div align="center">
  <img src="https://github.com/Efeyanik/Tabusal/blob/main/NEWLOGO.jpg)" alt="Burn Art Games Logo" width="150"/>

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

This project was built with clean code principles and scalability in mind. Key engineering highlights include:

### 1. `LocalizationManager.cs`
A robust dictionary-based architecture that handles multi-language data parsing in real-time. Instead of hardcoding text, the system reads from structured JSON files (`cards_en.json`, `cards_tr.json`), allowing for instant language toggling without scene reloads.

### 2. `BombModeGameManager.cs`
Handles the complex state-machine of the Bomb Mode. Manages asynchronous timers, score tracking, and team turn logic without dropping frames, ensuring the UI stays perfectly synced with the underlying game logic.

### 3. Responsive UI Controllers
Developed dynamic UI scaling and magnetic slider controls (value snapping) tailored for touch screens, ensuring that players can easily adjust settings like "Target Score" or "Round Time" with precision.

---

## 📸 Screenshots & UI Design

<div align="center">
  <img src="https://github.com/Efeyanik/Tabusal/blob/main/En_StdMode.jpg" width="250"/>
  <img src="https://github.com/Efeyanik/Tabusal/blob/main/En_bombmode.jpg" width="250"/>
  <img src="https://github.com/Efeyanik/Tabusal/blob/main/En_Settings.jpg" width="250"/>
</div>

---

## 🚀 Play The Game

TABUSAL is currently in the Closed Testing phase on the Google Play Console, preparing for its V1.0 Release. 

*If you are a recruiter or technical evaluator and wish to review the full source code (including proprietary content files) or receive a direct APK for testing, please contact me.*

## 📬 Contact

**Efe Yanık** Computer Engineering Student & Solo Developer  
[LinkedIn Profile](LINK_TO_YOUR_LINKEDIN) | **Burn Art Games**
