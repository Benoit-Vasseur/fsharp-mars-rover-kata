module Rover

open System.IO

type CoordinateUnit = uint8

let CoordinateUnit = uint8

type X = CoordinateUnit

type Y = CoordinateUnit

type Coordinates = X * Y

type Direction =
    | North
    | East
    | South
    | West

type Rover =
    { Coordinates: Coordinates
      Direction: Direction }

let createRover (coordinates: Coordinates) (direction: Direction): Rover =
    { Coordinates = coordinates
      Direction = direction }

type MovingCommand =
    | Forward
    | Backward

type TurningCommand =
    | ClockWise
    | CounterClockWise

type Command =
    | MoveForward
    | MoveBackward
    | TurnClockWise
    | TurnCounterClockWise

let move (direction: Direction) ((x, y): Coordinates) (movingCommand: MovingCommand): Coordinates =
    match (direction, movingCommand) with
    | (North, Forward)
    | (South, Backward) -> (x, y + CoordinateUnit 1)
    | (North, Backward)
    | (South, Forward) -> (x, y - CoordinateUnit 1)
    | (East, Forward)
    | (West, Backward) -> (x + CoordinateUnit 1, y)
    | (East, Backward)
    | (West, Forward) -> (x - CoordinateUnit 1, y)

let turn (direction: Direction) (turiningCommand: TurningCommand): Direction =
    match turiningCommand with
    | ClockWise ->
        match direction with
        | North -> East
        | East -> South
        | South -> West
        | West -> North
    | CounterClockWise ->
        match direction with
        | North -> West
        | East -> North
        | South -> East
        | West -> South

let commandRover (rover: Rover) (command: Command): Rover =
    match command with
    | MoveForward -> { rover with Coordinates = move rover.Direction rover.Coordinates Forward }
    | MoveBackward -> { rover with Coordinates = move rover.Direction rover.Coordinates Backward }
    | TurnClockWise -> { rover with Direction = turn rover.Direction ClockWise }
    | TurnCounterClockWise -> { rover with Direction = turn rover.Direction CounterClockWise }

let printRover (tw: TextWriter) (rover: Rover) =
    let x, y = rover.Coordinates
    tw.Write("Rover:(Coordinates=(X:{0} Y:{1}) ; Direction={2})", x, y, rover.Direction)
