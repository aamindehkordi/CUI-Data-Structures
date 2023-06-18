# Degrees of Separation Console Application

## Project Summary

This C# console application finds the shortest path between two actors using the concept of "degrees of separation". The project consists of two main parts: the GraphLibrary, which provides a graph data structure, and the KevinBaconGame, which uses the graph to find the shortest path between two actors.

## Purpose

The purpose of this project is to demonstrate the use of graph data structures and algorithms (such as breadth-first search) to solve real-world problems like finding the shortest path between two actors in a movie database.

## How to Use

1. Ensure that you have the .NET Core SDK installed on your machine.
2. Clone or download the project repository to your local machine.
3. Navigate to the project directory in a terminal or command prompt.
4. Make sure you have a text file containing a list of actors/movies and their connections (formatted as `Actor|Movie`). The provided `top250.txt` file can be used for testing purposes.
5. Build the project by running `dotnet build` in the terminal or command prompt.
6. Run the project by executing `dotnet run -- [Actor1] [Actor2]` in the terminal or command prompt, replacing `[Actor1]` and `[Actor2]` with the names of the actors you want to find the shortest path between. For example: `dotnet run -- "Kevin Bacon" "Harrison Ford"`
