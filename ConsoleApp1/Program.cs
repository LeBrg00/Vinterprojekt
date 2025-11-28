using System.Numerics;
using Raylib_cs;
Raylib.InitWindow(800, 600, "Waa");
Raylib.SetTargetFPS (60);
//variables and points
Vector2 Spawn1 = new Vector2(150, 490);
Vector2 Spawn2 = new Vector2(400, 490);
Vector2 Spawn3 = new Vector2(650, 490);
Vector2 ps = new(); // spawnpoint chosen for player attack
List<Vector2> enemyspanw=[]; // where the enemy attacks starts
List<Vector2> enemytarget =[]; // enemy attack end position
List<Vector2> starts =[]; // start position for player attack
List<Vector2> Ends =[]; //end position for player attack
List<int> attackTimer = []; //how long a player attack has existed
List<int> explosiontimmer = []; // how long a explosion has existed
List<int> enemmyattacktimmer=[]; // how long an enemy attack timmer has existed
int enemytimmer=0;  // time since an enemy has spawned
int enemyallow=90; // when an enemy gets to spawn again by time
int enemyallow2=1; // if an enemy can spawn
while(!Raylib.WindowShouldClose()){
if (Raylib.IsKeyPressed(KeyboardKey.Space)) // what spawn point the player attack should launsh from
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
    Vector2 mousePos = Raylib.GetMousePosition(); // where the player attack should target
    enemytimmer ++;
    if (enemytimmer==enemyallow && enemyallow2==1) {// determines if an enemy attack can spawn
    int enemyspanwx =50+Random.Shared.Next(700); // detemines spawn position
    Vector2 enemyspawn = new Vector2(enemyspanwx, 0);
    enemyspanw.Add(enemyspawn);
    //Console.WriteLine(enemyspawn);
    enemmyattacktimmer.Add(0); // starts counting how long the enemy attack have existed
    enemytimmer=0; // resets the timmer
    enemyallow = 30+Random.Shared.Next(90); // determines time to enemy 
    }
    if(Raylib.IsKeyPressed(KeyboardKey.E) && enemyallow2 == 1) //allows to manually turn of enemy spawns
    {
        enemyallow2=0;
    }
    else if (Raylib.IsKeyPressed(KeyboardKey.E)&& enemyallow2 == 0) //allows to manually turn on enemy spawns
    {
        enemyallow2=1;
    }
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Black);
    Raylib.DrawRectangle(0, 500, 800, 100, Color.Green); //terrain
    Raylib.DrawCircle(150, 510, 60, Color.Green); // terrain
    Raylib.DrawCircle(400, 510, 60, Color.Green); // terrain
    Raylib.DrawCircle(650, 510, 60, Color.Green); // terrain
    List<int> enemyremove=[];
    for (int i = 0; i < enemyspanw.Count; i++) //enemy draw and add to delete
    {
        enemmyattacktimmer[i]++;
        if (enemmyattacktimmer[i] > 300)
        {
            enemyremove.Add(i);
        }
        if (enemmyattacktimmer[i] < 300){
      //Raylib.DrawLineEx(enemyspanw[i],-(enemyspanw[i] - enemytarget[i])*enemmyattacktimmer[i]/300+enemyspanw[i], 5, Color.Red);
        }
    }
    for (int i = 0; i < enemyremove.Count; i++) //deleting finished enemy attack
    {
        enemmyattacktimmer.RemoveAt(enemyremove[i]);
        enemyspanw.RemoveAt(enemyremove[i]);
        //enemytarget.RemoveAt(enemyremove[i]);
    }


    if (Raylib.IsKeyPressed(KeyboardKey.Space))// determines player attack positions and timer

    {
        attackTimer.Add(0);
        starts.Add(ps);
        Ends.Add(mousePos);
        explosiontimmer.Add(0);
    }

    List<int> toRemove = []; // player attacks to remove
    for (int i = 0; i < starts.Count; i++) //player attacks to remove and draw
    {
        attackTimer[i]++;
        float Distance = Vector2.Distance(starts[i], Ends[i]);
        if (attackTimer[i] > Distance/10+20)
        {
            toRemove.Add(i);
        }
        if(attackTimer[i] <Distance/10){ // line for the until target frames
        Raylib.DrawLineEx(starts[i],-(starts[i] - Ends[i])*attackTimer[i]/(Distance/10)+starts[i], 5, Color.Blue);

        }
        else
        {// line for the last 20 frames
            Raylib.DrawLineEx(starts[i],Ends[i], 5, Color.Blue);
        }
        if (attackTimer[i] > Distance/10)
        { // explosion
        explosiontimmer[i] ++;
            Raylib.DrawCircleV(Ends[i], explosiontimmer[i]*2+20, Color.Blue);
        }
    }
    for (int i = 0; i < toRemove.Count; i++)// removal of old variables for player attacks
    {
        attackTimer.RemoveAt(toRemove[i]);
        starts.RemoveAt(toRemove[i]);
        Ends.RemoveAt(toRemove[i]);
        explosiontimmer.RemoveAt(toRemove[i]);
    }


        
    Raylib.EndDrawing();
}
