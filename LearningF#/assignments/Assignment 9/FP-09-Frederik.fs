module RayTracer

open System.Drawing
// #load "Vector.fs"
// #load "Point.fs"
open Point
open Vector
open Point

let discriminant a b c = b * b - 4.0 * a * c
let solve a b d = ((-b - (sqrt d))/(2.0 * a), (-b + sqrt d)/(2.0 * a))
// let circleIntersection a b c r = 
//     let d = discriminant b a (c - r * r)
//     if d < 0. then (infinity, infinity)
//     else solve a b d

let polynomialVariables (dx, dy, dz) (ox, oy, oz) r  = 
    let a = dx*dx + dy*dy + dz*dz
    let b = 2.*(ox*dx + oy*dy + oz*dz)
    let c = ox*ox + oy*oy + oz*oz - r*r
    (a, b, c) : float * float * float

let sendRays f (resX, resY) =
    for y in [0 .. resY-1] do
        for x in [0 .. resX-1] do
            f x y

let saveAsImage (colorArray : Color[,]) (fileName : string) = 
    let width = Array2D.length1 colorArray
    let height = Array2D.length2 colorArray
    use bitmap = new Bitmap(width, height)
    Array2D.iteri (fun x y c -> bitmap.SetPixel(x, y, c)) colorArray
    bitmap.Save(fileName, Imaging.ImageFormat.Png)
    ()

/// This function renders a sphere with the given radius and colour, centered at the origin,
/// and saves the rendered image into a file of the given name.
let renderSphere (radius : float)        // The radius of the sphere
                 (colour : Color)        // The colour of the sphere
                 (position : Point)      // The position of the camera
                 (lookat : Point)        // The point that the camera is looking at
                 (up : Vector)           // The up-vector for the camera
                 (zoom : float)          // Distance to the view plane
                 (width : float)         // Width of the view plane in units
                 (height : float)        // Height of the view plane in units
                 (resX : int)            // The horizontal resolution of the view plane  
                 (resY : int)            // The vertical resolution of the view plane
                 (fileName : string)     // Name of the file to save the rendered image
                 : unit
  = 
    let pixelWidth  = width / float resX 
    let pixelHeight = height/ float resY
    let w = Vector.normalise (Point.distance lookat position)
    let u = Vector.normalise (Vector.crossProduct up w)  
    let v = Vector.crossProduct w u

    printfn "up= %s" (string (Vector.getCoord up))
    printfn "position= %s" (string (Point.getCoord position))
    printfn "lookat= %s" (string (Point.getCoord lookat))
    printfn "w(z)= %s" (string (Vector.getCoord w))
    printfn "u(y)= %s" (string (Vector.getCoord u))
    printfn "v(x)= %s" (string (Vector.getCoord v))


    let colorArray = Array2D.create resX resY Color.Black

    let hit x y = 
        let px = pixelWidth  * (float x - float resX/2.0 + 0.5)
        let py = pixelHeight * (float y - float resY/2.0 + 0.5)
        let direction = (px * u) + (py * v) - (zoom * w)

        let (a, b, c) = polynomialVariables (Point.getCoord position) (Vector.getCoord direction) radius
        if x = 0 && y = 0 then printfn "%s" (string (a,b,c))
        let d = discriminant a b c
        if x = 0 && y = 0 then printfn "%f\n" d   
        if d < 0. then None
        else
            let (t1, t2) = solve a b d
            let t = if t1 < 0. && t2 < 0. then t1
                    else if t1 < 0. then t2
                    else if t2 < 0. then t1
                    else min t1 t2                     

            if t < 0. then None
            else Some t

    let colorIfHit x y = function
        | None -> ()
        | Some _ -> colorArray.[x,y] <- colour 

    sendRays (fun x y -> hit x y |> colorIfHit x y) (resX, resY)
    saveAsImage colorArray fileName
    printfn ""
        
[<EntryPoint>]
let main argv = 
    // red sphere, with the camera in front of it
    renderSphere 2.0 Color.Red (mkPoint 0.0 0.0 4.0) (mkPoint 0.0 0.0 0.0) (mkVector 0.0 1.0 0.0) 1.0 2.0 2.0 512 512 "output/front.png"

    // larger golden sphere, with the camera in front of it
    // renderSphere 3.0 Color.Gold (mkPoint 0.0 0.0 4.0) (mkPoint 0.0 0.0 0.0) (mkVector 0.0 1.0 0.0) 1.0 2.0 2.0 512 512 "output/big.png"

    // // green sphere, with the camera in front of it
    // renderSphere 2.0 Color.Green (mkPoint 0.0 0.0 4.0) (mkPoint 0.0 2.0 0.0) (mkVector 0.0 1.0 0.0) 1.0 2.0 2.0 512 512 "output/up.png"

    // // blue sphere, with the camera inside of it
    // renderSphere 2.0 Color.Blue (mkPoint 0.0 0.0 0.0) (mkPoint 0.0 0.0 -4.0) (mkVector 0.0 1.0 0.0) 1.0 2.0 2.0 512 512 "output/inside.png"

    // // yellow sphere, with the camera behind it (pointing in the other direction)
    // renderSphere 2.0 Color.Yellow (mkPoint 0.0 0.0 -4.0) (mkPoint 0.0 0.0 -8.0) (mkVector 0.0 1.0 0.0) 1.0 2.0 2.0 512 512 "output/behind.png"

    0