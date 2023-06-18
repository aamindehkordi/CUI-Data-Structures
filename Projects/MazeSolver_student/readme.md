# Introduction

The MazeSolver project is a C# program that utilizes a maze-solving agent to navigate and solve mazes. The program provides a framework for generating random mazes or loading custom mazes from external files. The maze-solving agent, named "Franc," explores the maze and makes decisions based on its surroundings to find the optimal path to the maze's finish point.

## Maze-Solving Agent

The maze-solving agent, named Franc, is an implementation of the abstract class `MazeAgent`. Franc is capable of navigating through mazes, making decisions based on its current position and surroundings, and finding the shortest path to the maze's finish point. The agent uses a combination of path exploration and backtracking algorithms to efficiently solve the maze.

## Initialization

To initialize the maze-solving agent, an instance of the `Franc` class is created. The agent's nickname and glyph are set, and the necessary variables are initialized. The `Initialize` method is called to set the starting space and maze sweep.

## Exploration

The exploration process begins with the agent making its first move into the maze. The agent explores the maze by moving through hallways and leaving breadcrumbs to track its path. It evaluates its surroundings at each step and makes decisions based on the available paths. The agent prioritizes unexplored paths and backtracks when it reaches dead ends. It employs a stack of breadcrumbs to keep track of its path and junctions in the maze.

## Additional Utility Functions

The MazeSolver project also includes additional utility functions to generate random mazes or load custom mazes from external files. These functions allow users to create mazes of various sizes and complexities and customize the maze-solving experience.
