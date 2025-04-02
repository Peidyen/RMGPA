# Morphogenetic Programming – First Working Prototype

This version of the `RMGPA` (Ronco’s Morphogenetic Programming Architecture) prototype simulates a basic morphogenetic "tissue" grid of virtual cells. Each cell is equipped with swappable functional units called *organelles*, and the system evaluates a simple goal for each one.

## 🧬 What This Code Does

### ✅ 1. **Defines the core biological metaphor**
- `Cell`: A virtual construct with identity, role, memory, location, and organelles
- `Role`: Functional identity of a cell (Processor, Memory, Sensor, Control)
- `Organelle`: A pluggable unit of functionality that can modify a cell's internal state
- `Location`: Coordinates in a logical grid (e.g., `Grid(x, y)`)

### ✅ 2. **Builds a 10x10 virtual tissue**
- Generates 100 unique `Cell` records
- Positions them in a grid layout from `(0, 0)` to `(9, 9)`
- Assigns roles based on position: diagonal = Control, modulo logic = Memory or Sensor, else = Processor
- Attaches the same 3 organelles to each cell: `processor`, `memory`, `signal`

### ✅ 3. **Evaluates a placeholder goal**
- Applies `interpretGoal` to every cell
- Currently just prints out the ID and goal
- Will later include structured goal interpretation (e.g., energy management, memory tasks, signaling)

### ✅ 4. **Displays an ASCII tissue layout**
- Each cell is represented by a single letter:
  - `P`: Processor
  - `M`: Memory
  - `S`: Sensor
  - `C`: Control
- Arranged visually like a grid to show structure at a glance

Example output:
```
P M P S P M P S P M 
M C M P S P M P S P 
P M P S C M P S P M 
...
```

---

## 🔄 Next Steps

This lays the groundwork for:
- Stateful simulations across ticks
- Energy/resource management
- Signal propagation and neighbor awareness
- Evolutionary adaptation or cell division
- DSL integration for runtime goals and learning behavior

---

## 📁 File Overview

| File         | Purpose |
|--------------|---------|
| `Program.fs` | Main entrypoint; builds tissue, runs evaluation, and prints grid |
| `Cell.fs`    | Defines `Cell`, `Role`, `Location`, and `Organelle` (merged for dependency resolution) |
| `Tissue.fs`  | Builds example cells, defines processor/memory/signal organelles |
| `Evaluator`  | Stubbed evaluator for goal interpretation (currently inline) |

---

## 🧪 Status

✅ First working run  
✅ 100-cell grid initialized  
✅ Role-based layout visualized  
✅ Goal evaluation framework in place  
🔜 Real evaluator logic, interaction, and visual state tracking
