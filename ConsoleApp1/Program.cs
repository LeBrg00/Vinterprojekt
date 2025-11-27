using System.Numerics;
using Raylib_cs;
Raylib.InitWindow(800, 600, "Waa");
Raylib.SetTargetFPS (60);
//variables
Vector2 Spawn1 = new Vector2(150, 490);
Vector2 Spawn2 = new Vector2(400, 490);
Vector2 Spawn3 = new Vector2(650, 490);
Vector2 ps = new();
int Attack = 100;
Vector2 end = new();
int end2;
List<Vector2> starts =[];
List<Vector2> Ends =[];
List<int> attackTimer = [];
while(!Raylib.WindowShouldClose()){

    //player spawnpoints/
    //player attacks(
    //detect input
if (Raylib.IsKeyPressed(KeyboardKey.Space))
    {
        int S = Raylib.GetMouseX();
        if(S < 266)
        {
            ps = Spawn1;
        }
        else if(S < 532)
        {
            ps = Spawn2;
        }
        else
        {
            ps = Spawn3;
        }
    }
    Vector2 mousePos = Raylib.GetMousePosition();
    //pick spawnpoint
    //line from spawn to input

    //explosion at input

    //)

    //enemy attacks missiles(
    //spawn point

    //line towards a target

    // if 
    
    //)
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Black);
    Raylib.DrawRectangle(0, 500, 800, 100, Color.Green);
    Raylib.DrawCircle(150, 510, 60, Color.Green);
    Raylib.DrawCircle(400, 510, 60, Color.Green);
    Raylib.DrawCircle(650, 510, 60, Color.Green);
    if (Raylib.IsKeyPressed(KeyboardKey.Space))

    {
        attackTimer.Add(0);
        starts.Add(ps);
        Ends.Add(mousePos);
        
    }
   // if (Attack < 45 || Raylib.IsKeyPressed(KeyboardKey.Space))
  //  {
  //      Vector2 start = ps;
   //     Raylib.DrawLineEx(start, end, 5, Color.Blue);
   //     Attack ++;
   //     if (Attack > 30)
   //     {
   //         Raylib.DrawCircleV(end, 40, Color.Blue);
   //     }
   // }

    List<int> toRemove = [];
    for (int i = 0; i < starts.Count; i++)
    {
        attackTimer[i]++;
        if (attackTimer[i] > 45)
        {
            toRemove.Add(i);
        }
        if(attackTimer[i] <30){
        float startsx=starts[i].X;
        float startsy=starts[i].Y;
        float endsx=Ends[i].X;
        float endsy=Ends[i].Y;
        float x= (endsx-startsx)*(attackTimer[i]/30);
        float y= (endsy-startsx)*(attackTimer[i]/30);
        Vector2 xy = new Vector2(x, y);
        Raylib.DrawLineEx(starts[i],Ends[i], 5, Color.Blue);
        Console.WriteLine(Ends[i]);

        }
        else
        {
            Raylib.DrawLineEx(starts[i],Ends[i], 5, Color.Blue);
        }
        if (attackTimer[i] > 30)
        {
            Raylib.DrawCircleV(Ends[i], attackTimer[i]*2-40, Color.Blue);
        }
    }
    for (int i = 0; i < toRemove.Count; i++)
    {
        attackTimer.RemoveAt(toRemove[i]);
        starts.RemoveAt(toRemove[i]);
        Ends.RemoveAt(toRemove[i]);
    }


        
    Raylib.EndDrawing();
}
