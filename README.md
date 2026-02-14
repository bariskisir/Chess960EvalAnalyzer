# Chess960 "Luck Factor" Analysis

> **About Chess960:** The starting position is randomly selected from 960 possibilities before the match, with two rules: Bishops must be on opposite colors, and the King must be between the Rooks.
>
> *This analysis was inspired by the **FIDE Freestyle Chess World Championship** (Feb 13-15, 2026).*

## Project Overview
This hobby project analyzes the inherent fairness of **Chess960 starting positions** using **Stockfish 18** (Depth 25, Intel i5-1135G7, 32GB RAM). The goal is to quantify how much "luck" is involved based on the initial engine evaluation.

## ⚠️ Running the Analysis
Due to GitHub file size limits, Stockfish 18 is not included. 
1. Download **Stockfish 18** from [stockfishchess.org](https://stockfishchess.org/download/).
2. Extract the executable to `src/Chess960EvalAnalyzer`.

## Key Findings
White *always* has an advantage, but the eval varies wildly:
*   **Standard Chess (ID 518)**: **+0.32** (Baseline).
*   **Best Case for White**: **+0.80** (Nearly decisive).
*   **Worst Case for White**: **-0.01** (Dead equal).

### 🏛️ White's Top 10 (Highest Advantage)
Eval: +0.74 to +0.80. Randomization gifts White a massive head start.

| ID | Eval | Placement | Pieces |
| :--- | :--- | :--- | :--- |
| **80** | **+0.80** | `BBNNRKRQ` | ♗♗♘♘♖♔♖♕ |
| 477 | +0.79 | `RNNBKRBQ` | ♖♘♘♗♔♖♗♕ |
| 604 | +0.77 | `RBQNKRBN` | ♖♗♕♘♔♖♗♘ |
| 848 | +0.77 | `BBRKNRNQ` | ♗♗♖♔♘♖♘♕ |
| 879 | +0.77 | `QRKRNNBB` | ♕♖♔♖♘♘♗♗ |
| 935 | +0.77 | `RKBRNQNB` | ♖♔♗♖♘♕♘♗ |
| 794 | +0.75 | `RQKNBBRN` | ♖♕♔♘♗♗♖♘ |
| 176 | +0.74 | `BBNRNKRQ` | ♗♗♘♖♘♔♖♕ |
| 557 | +0.74 | `RNKBNQBR` | ♖♘♔♗♘♕♗♖ |

### 🛡️ Black's Top 10 (Least Disadvantage)
Eval: -0.01 to +0.12. Effectively a draw from move 1.

| ID | Eval | Placement | Pieces |
| :--- | :--- | :--- | :--- |
| **644** | **-0.01** | `RBBNKRQN` | ♖♗♗♘♔♖♕♘ |
| 247 | +0.07 | `NRBKQNRB` | ♘♖♗♔♕♘♖♗ |
| 497 | +0.08 | `BRQBNKNR` | ♗♖♕♗♘♔♘♖ |
| 774 | +0.09 | `QRBKNBRN` | ♕♖♗♔♘♗♖♘ |
| 204 | +0.09 | `QBNRKNBR` | ♕♗♘♖♔♘♗♖ |
| 194 | +0.10 | `BQNRKBNR` | ♗♕♘♖♔♗♘♖ |
| 603 | +0.11 | `RQNKBRNB` | ♖♕♘♔♗♖♘♗ |
| 29 | +0.11 | `NQNBRKBR` | ♘♕♘♗♖♔♗♖ |
| 593 | +0.12 | `BRQBNKRN` | ♗♖♕♗♘♔♖♘ |
| 269 | +0.12 | `NRKBNQBR` | ♘♖♔♗♘♕♗♖ |

---

## 🧐 Developer's Commentary
1.  **The "Luck Gap"**: The difference between a **+0.80** start (ID 80) and a **0.00** start (ID 644) is huge. In a tournament, drawing ID 80 is a significant stroke of luck compared to the more "fair" ID 644.
2.  **Piece Synergy**: High-eval positions often feature bishops on long diagonals early or connected rooks (`BBNNRKRQ`). Low-eval positions often have clumped pieces blocking natural development.
3.  **Theoretical vs. Practical**: While many positions evaluate > +0.32, the lack of opening theory ("First Move Disadvantage") can make these hard to convert in practice compared to classical chess preparation.

## Download Results
[Download results.csv](https://raw.githubusercontent.com/bariskisir/Chess960EvalAnalyzer/refs/heads/master/src/Chess960EvalAnalyzer/results.csv)

Columns: `index, eval, placement`
