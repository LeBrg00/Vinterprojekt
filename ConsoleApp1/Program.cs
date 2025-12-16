using System.Numerics;
using Raylib_cs;
Raylib.InitWindow(800, 600, "Waa");
Raylib.SetTargetFPS(60);
//variables and points
int score=0;
Vector2 Spawn1 = new Vector2(150, 490);
Vector2 Spawn2 = new Vector2(400, 490);
Vector2 Spawn3 = new Vector2(650, 490);
Vector2 ps = new(); // spawnpoint chosen for player attack
List<Vector2> enemyspanw = []; // where the enemy attacks starts
List<Vector2> enemytarget = []; // enemy attack end position
Vector2 targetextra = new Vector2(20, 40); //Needed to make the enemy attack hit the center of the city´s
List<Vector2> starts = []; // start position for player attack
List<Vector2> Ends = []; //end position for player attack
List<int> attackTimer = []; //how long a player attack has existed
List<int> explosiontimmer = []; // how long a explosion has existed
List<int> enemmyattacktimmer = []; // how long an enemy attack timmer has existed
List<int> enemyexpl = []; // how long the enemys explosion has existed
int enemytimmer = 0;  // time since an enemy has spawned
int enemyallow = 90; // when an enemy gets to spawn again by time
int enemyallow2 = 1; // if an enemy can spawn
List<int> bbb = [1,1,1,1,1,1]; // Burn baby burn(if a city is destroyd(When all reach zero its game over)) 
int loss=0;
Vector2 size = new Vector2(60,40);
//city locations
List<Vector2> city = [new Vector2(20, 440), new Vector2(220, 440), new Vector2(290, 440), new Vector2(470, 440), new Vector2(540, 440), new Vector2(740, 440)];
List<Vector2> citytarg = [new Vector2(20, 440), new Vector2(220, 440), new Vector2(290, 440), new Vector2(470, 440), new Vector2(540, 440), new Vector2(740, 440)];
Texture2D citys = Raylib.LoadTexture("962317.2_city.missle1.png");//city texture
Texture2D citysburned=Raylib.LoadTexture("15857756.900000036_city burned.png"); //burn baby burn texture
Texture2D missile = Raylib.LoadTexture("missile.png"); //missile texture
while (!Raylib.WindowShouldClose())
{
    if(loss==0){
    if (Raylib.IsKeyPressed(KeyboardKey.Space)) // what spawn point the player attack should launsh from
    {
        int S = Raylib.GetMouseX();
        if (S < 266)
        {
            ps = Spawn1;
        }
        else if (S < 532)
        {
            ps = Spawn2;
        }
        else
        {
            ps = Spawn3;
        }
    }
    Vector2 mousePos = Raylib.GetMousePosition(); // where the player attack should target
    enemytimmer++;

    if (enemytimmer == enemyallow && enemyallow2 == 1)
    {// determines if an enemy attack can spawn
        int enemyspanwx = 50 + Random.Shared.Next(700); // detemines spawn position
        Vector2 enemyspawn = new Vector2(enemyspanwx, 0);
        enemyspanw.Add(enemyspawn);
        enemyexpl.Add(2);
        enemytarget.Add(citytarg[Random.Shared.Next(citytarg.Count)] + targetextra);// Target for attack
        enemmyattacktimmer.Add(0); // starts counting how long the enemy attack have existed
        enemytimmer = 0; // resets the timmer
        enemyallow = 15 + Random.Shared.Next(75); // determines time to next attack
    }
    if (Raylib.IsKeyPressed(KeyboardKey.E) && enemyallow2 == 1) //allows to manually turn of enemy spawns
    {
        enemyallow2 = 0;
    }
    else if (Raylib.IsKeyPressed(KeyboardKey.E) && enemyallow2 == 0) //allows to manually turn on enemy spawns
    {
        enemyallow2 = 1;
    }

    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Black);
    Raylib.DrawRectangle(0, 500, 800, 100, Color.Green); //terrain
    Raylib.DrawCircle(150, 510, 60, Color.Green); // terrain
    Raylib.DrawCircle(400, 510, 60, Color.Green); // terrain
    Raylib.DrawCircle(650, 510, 60, Color.Green); // terrain
    for (int i = 0; i < city.Count; i++) //"Draws" the city's
    {
        Rectangle cit = new (city[i], size);
        if(bbb[i]==1){ // if not cooked
        Raylib.DrawTextureEx(citys, city[i], 0, 2, Color.White);
        }
        else //if cooked
        {
        Raylib.DrawTextureEx(citysburned, city[i], 0, 2, Color.White);
        }
        for (int J = 0; J <enemyspanw.Count; J++)//cookin
        {
            Vector2 enemyPos = -(enemyspanw[J] - enemytarget[J]) * enemmyattacktimmer[J] / 300 + enemyspanw[J];
            if (Raylib.CheckCollisionPointRec(enemyPos,cit))
            {
               bbb[i]=0; 
            }
        }
    }

    List<int> enemyremove = [];
    for (int i = 0; i < enemyspanw.Count; i++) //enemy draw and add to delete
    {
        //Vector2 enemyPos = enemyspanw[i];
        Vector2 enemyPos = -(enemyspanw[i] - enemytarget[i]) * enemmyattacktimmer[i] / 300 + enemyspanw[i];
        for (int j = 0; j < Ends.Count; j++)
        {
            Vector2 circleCenter = Ends[j];
            float radius = explosiontimmer[j] * 2 + 20;

            if (Raylib.CheckCollisionPointCircle(enemyPos, circleCenter, radius))
            {
                enemyremove.Add(i);
                score ++;
            }
        }


        enemmyattacktimmer[i]++;
        if (enemmyattacktimmer[i] > 360) // Select enemy to delete
        {

            enemyremove.Add(i);
        }
        if (enemmyattacktimmer[i] < 300)
        { // Draws the enemy line
            Raylib.DrawLineEx(enemyspanw[i], -(enemyspanw[i] - enemytarget[i]) * enemmyattacktimmer[i] / 300 + enemyspanw[i], 5, Color.Red);
        }
        else
        {
            Raylib.DrawLineEx(enemyspanw[i], enemytarget[i], 5, Color.Red);
            enemyexpl[i]++;
            Raylib.DrawCircleV(enemytarget[i], enemyexpl[i] / 2, Color.Red); // Explosion
        }

    }
    for (int i = 0; i < enemyremove.Count; i++) //deleting finished enemy attack 
    {
        enemmyattacktimmer.RemoveAt(enemyremove[i]);
        enemyspanw.RemoveAt(enemyremove[i]);
        enemytarget.RemoveAt(enemyremove[i]);
        enemyexpl.RemoveAt(enemyremove[i]);
    }


    if (Raylib.IsKeyPressed(KeyboardKey.Space))// determines player attack positions and timer

    {
        attackTimer.Add(0);
        starts.Add(ps);
        Ends.Add(mousePos);
        explosiontimmer.Add(0);
    }

    List<int> toRemove = []; // player attacks to remove 
    for (int i = 0; i < starts.Count; i++) //player attacks to remove and draw // hjälp från micke
    {
        attackTimer[i]++;
        float Distance = Vector2.Distance(starts[i], Ends[i]);
        if (attackTimer[i] > Distance / 10 + 20)
        {
            toRemove.Add(i);
        }
        if (attackTimer[i] < Distance / 10)
        { // line for the until target frames
            Raylib.DrawLineEx(starts[i], -(starts[i] - Ends[i]) * attackTimer[i] / (Distance / 10) + starts[i], 5, Color.Blue);

        }
        else
        {// line for the last 20 frames
            Raylib.DrawLineEx(starts[i], Ends[i], 5, Color.Blue);
        }
        if (attackTimer[i] > Distance / 10)
        { // explosion
            explosiontimmer[i]++;
            Raylib.DrawCircleV(Ends[i], explosiontimmer[i] * 2 + 20, Color.Blue);
        }

    }
    for (int i = 0; i < toRemove.Count; i++)// removal of old variables for player attacks // av micke
    {
        attackTimer.RemoveAt(toRemove[i]);
        starts.RemoveAt(toRemove[i]);
        Ends.RemoveAt(toRemove[i]);
        explosiontimmer.RemoveAt(toRemove[i]);
    }

    Raylib.DrawText("score: " +score,50,530,60,Color.White);// score

    if (bbb.Sum() == 0) // end condition
    {
        loss=1;
    }
    Raylib.EndDrawing();
    }
    else if (loss == 1) //Game over screen
    {
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Black);
    Raylib.DrawRectangle(0, 500, 800, 100, Color.Green); //terrain
    Raylib.DrawCircle(150, 510, 60, Color.Green); // terrain
    Raylib.DrawCircle(400, 510, 60, Color.Green); // terrain
    Raylib.DrawCircle(650, 510, 60, Color.Green); // terrain
    for (int i = 0; i < city.Count; i++) //"Draws" the city's
    {
        Rectangle cit = new (city[i], size);
        if(bbb[i]==1){
        Raylib.DrawTextureEx(citys, city[i], 0, 2, Color.White);
        }
        else
        {
        Raylib.DrawTextureEx(citysburned, city[i], 0, 2, Color.White);
        }
    }
    Raylib.DrawText("Game Over",250,150,60,Color.White);// Game over
    Raylib.DrawText("Score: " +score,280,220,50,Color.White);// score
    Raylib.EndDrawing();
    }
}