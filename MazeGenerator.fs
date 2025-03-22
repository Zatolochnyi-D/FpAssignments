module FpAssignments.MazeGenerator

open System
open System.Collections.Generic
open System.Linq
open SimpleMath
open GUI
open Window
open Colors

type NodeType = Entrance | Exit | Maze
type Node = { x: int; y: int; neighbors: List<Node>; nodeType: NodeType}
type Maze = { width: int; height: int; nodes: List<List<Node>> }

let randomDirection maze (random: Random) x y =
    let allowedDirections = List<int * int>()
    if x = 0 |> not  then allowedDirections.Add (-1, 0) 
    if x = maze.width - 1 |> not then allowedDirections.Add (1, 0) 
    if y = 0 |> not then allowedDirections.Add (0, -1) 
    if y = maze.height - 1 |> not then allowedDirections.Add (0, 1) 
    allowedDirections.[random.Next allowedDirections.Count]

let fillBaseMaze maze = 
    for _ = 0 to maze.height - 1 do  
        maze.nodes.Add (List<Node>())
    maze.nodes.[0].Add { x = 0; y = 0; neighbors = List<Node> (); nodeType = Entrance }
    for y = 1 to maze.height - 1 do
        let node = { x = 0; y = y; neighbors = List<Node> (); nodeType = Maze }
        node.neighbors.Add maze.nodes.[y - 1].[0]
        maze.nodes.[y].Add node
    for y = 0 to maze.height - 1 do  
        for x = 1 to maze.width - 1 do
            let node = { x = x; y = y; neighbors = List<Node> (); nodeType = Maze}
            node.neighbors.Add maze.nodes.[y].[x - 1]
            maze.nodes.[y].Add node
    let x, y = maze.width - 1, maze.height - 1
    let node = { x = x; y = y; neighbors = List<Node> (); nodeType = Exit}
    node.neighbors.Add maze.nodes.[y].[x - 1]
    maze.nodes.[y].[x] <- node
    maze

let randomizeMaze iterations maze =
    let r = Random ()
    let rec recursiveRandomizer iterations currentInteration maze rootX rootY =
        if currentInteration <> iterations then
            let shiftX, shiftY = randomDirection maze r rootX rootY
            let x, y = rootX + shiftX, rootY + shiftY
            printfn $"{x} {y}"
            let newRoot = maze.nodes.[y].[x]
            newRoot.neighbors.Clear ()
            let oldRoot = maze.nodes.[rootY].[rootX]
            oldRoot.neighbors.Add newRoot
            recursiveRandomizer iterations (currentInteration + 1) maze x y
        else
            maze
    recursiveRandomizer iterations 0 maze 0 0

let createBothSidesConnections maze = 
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

let mazeTile nodeType index =
    let result = List<string> ()
    match nodeType with
        | Entrance -> 
            match index with
                | 0b0010 -> result.Add "#0#";
                            result.Add "#  ";
                            result.Add "###"
                            result
                | 0b0100 -> result.Add "#0#";
                            result.Add "# #";
                            result.Add "# #"
                            result
                | 0b0110 -> result.Add "#0#";
                            result.Add "#  ";
                            result.Add "# #"
                            result
                | _ ->      result.Add "XXX";
                            result.Add "XXX";
                            result.Add "XXX"
                            result
        | Exit ->
            match index with
                | 0b0001 -> result.Add "# #";
                            result.Add "# #";
                            result.Add "#0#"
                            result
                | 0b1000 -> result.Add "###";
                            result.Add "  #";
                            result.Add "#0#"
                            result
                | 0b1001 -> result.Add "# #";
                            result.Add "  #";
                            result.Add "#0#"
                            result
                | _ ->      result.Add "XXX";
                            result.Add "XXX";
                            result.Add "XXX"
                            result
        | Maze ->
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
                | _ ->      result.Add "XXX";
                            result.Add "XXX";
                            result.Add "XXX"
                            result

let generateRect window maze =
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
            let tile = 0 |> sides |> mazeTile node.nodeType
            for i = 0 to tile.Count - 1 do
                lines.[y * 3 + i] <- lines.[y * 3 + i] + tile[i]
    createDrawRect (0, 0) (windowSize window) lines black white

let generateMaze window width height iterations = 
    let maze = { width = width; height = height; nodes = List<List<Node>>()}
    fillBaseMaze maze |> randomizeMaze iterations |> createBothSidesConnections |> generateRect window
            