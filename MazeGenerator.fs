module FpAssignments.MazeGenerator

open System
open System.Collections.Generic
open System.Linq
open SimpleMath
open GUI

type Node = { x: int; y: int; neighbors: List<Node> }
type Maze = { width: int; height: int; nodes: List<List<Node>> }

let randomDirection maze (random: Random) x y =
    let allowedDirections = List<int * int>()
    if x = 0 |> not  then allowedDirections.Add (-1, 0) 
    if x = maze.width - 1 |> not then allowedDirections.Add (1, 0) 
    if y = 0 |> not then allowedDirections.Add (0, -1) 
    if y = maze.height - 1 |> not then allowedDirections.Add (0, 1) 
    allowedDirections.[random.Next allowedDirections.Count]

let mazeBase maze = 
    for _ = 0 to maze.height - 1 do  
        maze.nodes.Add (List<Node>())
    maze.nodes.[0].Add { x = 0; y = 0; neighbors = List<Node> () }
    for y = 1 to maze.height - 1 do
        let node = { x = 0; y = y; neighbors = List<Node> () }
        node.neighbors.Add maze.nodes.[y - 1].[0]
        maze.nodes.[y].Add node
    for y = 0 to maze.height - 1 do  
        for x = 1 to maze.width - 1 do
            let node = { x = x; y = y; neighbors = List<Node> () }
            node.neighbors.Add maze.nodes.[y].[x - 1]
            maze.nodes.[y].Add node
    maze

let randomizedMaze iterations maze =
    let r = Random ()
    let mutable currentRootX, currentRootY = 0, 0
    for _ = 0 to iterations - 1 do
        let shiftX, shiftY = randomDirection maze r currentRootX currentRootY
        let x, y = currentRootX + shiftX, currentRootY + shiftY
        let newRoot = maze.nodes.[y].[x]
        newRoot.neighbors.Clear ()
        let oldRoot = maze.nodes.[currentRootY].[currentRootX]
        oldRoot.neighbors.Add newRoot
        currentRootX <- x
        currentRootY <- y
    maze

let connectedMaze maze = 
    for line in maze.nodes do
        for node in line do
            for neighbor in node.neighbors do
                neighbor.neighbors.Add node
    for line in maze.nodes do
        for node in line do
            let distinctNeighbors = (node.neighbors.Distinct()).ToList()
            node.neighbors.Clear ()
            node.neighbors.AddRange distinctNeighbors
    maze

let mazeTile index =
    let result = List<string> ()
    match index with
        | 0b0001 -> result.Add "# #";
                    result.Add "# #";
                    result.Add "###"
                    result
        | 0b0010 -> result.Add "###";
                    result.Add "#  ";
                    result.Add "###"
                    result
        | 0b0100 -> result.Add "###";
                    result.Add "# #";
                    result.Add "# #"
                    result
        | 0b1000 -> result.Add "###";
                    result.Add "  #";
                    result.Add "###"
                    result
        | 0b0011 -> result.Add "# #";
                    result.Add "#  ";
                    result.Add "###"
                    result
        | 0b1001 -> result.Add "# #";
                    result.Add "  #";
                    result.Add "###"
                    result
        | 0b0110 -> result.Add "###";
                    result.Add "#  ";
                    result.Add "# #"
                    result
        | 0b1100 -> result.Add "###";
                    result.Add "  #";
                    result.Add "# #"
                    result
        | 0b0101 -> result.Add "# #";
                    result.Add "# #";
                    result.Add "# #"
                    result
        | 0b1010 -> result.Add "###";
                    result.Add "   ";
                    result.Add "###"
                    result
        | 0b1011 -> result.Add "# #";
                    result.Add "   ";
                    result.Add "###"
                    result
        | 0b1110 -> result.Add "###";
                    result.Add "   ";
                    result.Add "# #"
                    result
        | 0b0111 -> result.Add "# #";
                    result.Add "#  ";
                    result.Add "# #"
                    result
        | 0b1101 -> result.Add "# #";
                    result.Add "  #";
                    result.Add "# #"
                    result
        | 0b1111 -> result.Add "# #";
                    result.Add "   ";
                    result.Add "# #"
                    result
        | _ ->      result.Add "###";
                    result.Add "###";
                    result.Add "###"
                    result

let generateRects maze =
    let lines = List<string> ()
    for _ in maze.nodes do
        lines.Add ""
        lines.Add ""
        lines.Add ""
    let yCount = counter 0
    for line in maze.nodes do
        let y = yCount ()
        for node in line do
            let sides = sum ()
            for neighbor in node.neighbors do
                let i =
                    match node.x - neighbor.x, node.y - neighbor.y with
                    | 0, 1 -> 0b0001 //0b_left_bottom_right_top
                    | -1, 0 -> 0b0010
                    | 0, -1 -> 0b0100
                    | 1, 0 -> 0b1000
                    | _ -> 0b0000
                sides i |> ignore
            let tile = 0 |> sides |> mazeTile
            for i = 0 to tile.Count - 1 do
                lines.[y * 3 + i] <- lines.[y * 3 + i] + tile[i]
    createDrawRect TopCenter TopCenter (0, 0) lines

let generateMaze width height iterations = 
    let maze = { width = width; height = height; nodes = List<List<Node>>()}
    mazeBase maze |> randomizedMaze iterations |> connectedMaze |> generateRects
            