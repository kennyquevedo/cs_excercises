using System;
using System.Collections.Generic;
using System.Linq;

public class CloudGame
{
    // static int jumpingOnClouds(int[] c)
    // {
    //     //Build dictionary to add step to each cloud
    //     int countStep = 0;
    //     var clouds = (from n in c.ToList()
    //                   select new
    //                   {
    //                       k = countStep++,
    //                       n
    //                   }).ToDictionary(i => i.k, i => i.n);

    //     Console.WriteLine("-----ORIGINAL DICTIONARY------");
    //     Console.WriteLine(string.Join(',', clouds.Keys.ToArray()));
    //     Console.WriteLine(string.Join(',', clouds.Values.ToArray()));

    //     //remove all thunderclouds.
    //     var cumulusCloud = clouds.Where(s => s.Value == 0).ToDictionary(d => d.Key, d => d.Value);

    //     Console.WriteLine("-----CUMULUS CLOUDS------");
    //     Console.WriteLine(string.Join(',', cumulusCloud.Keys.ToArray()));
    //     Console.WriteLine(string.Join(',', cumulusCloud.Values.ToArray()));

    //     var cumulusCloudSteps = cumulusCloud.Select(cc => cc.Key).ToList();
    //     Console.WriteLine("-----CUMULUS CLOUDS STEPS------");
    //     Console.WriteLine(string.Join(',', cumulusCloudSteps.ToArray()));

    //     //validate best steps to goal.
    //     for (int i = 0; i < cumulusCloudSteps.Count; i++)
    //     {

    //     }

    //     return cumulusCloud.Count - 1;
    // }
    static int jumpingOnClouds(int[] c)
    {
        int jumps = 0;

        if (c.Length >= 2 && c.Length <= 100)
        {
            jumps = JumpStep(0, c[0], jumps, c);
        }

        Console.WriteLine("-----jumpingOnClouds------");
        Console.WriteLine(jumps);

        return jumps;

    }


    static int JumpStep(int i, int c, int jumpsOnCloud, int[] clouds)
    {
        //jumpsOnCloud, has the latest jumps
        int jumps = jumpsOnCloud;

        //if the current cloud if 0 and is not the first position we need to jump
        if (c == 0)
        {
            if (i > 0)
                jumps++;
        }

        //We need to validate the cloud in the next position
        int j = i + 1;
        if (j <= clouds.Length - 1)
        {
            //We validating if the next step is a valid cloud, if it, we "jump" to that position.
            int k = j + 1;
            if (k <= (clouds.Length - 1) && clouds[k] == 0)
            {
                //next value
                j = k;
            }

            //Call self function (recursive) to validate next cloud position.
            jumps = JumpStep(j, clouds[j], jumps, clouds);
        }

        return jumps;
    }

    public CloudGame()
    {
        // int[] clouds = new int[] { 0, 0, 1, 0, 0, 1, 0, };
        int[] clouds = new int[] { 0, 0, 0, 0, 1, 0 };
        // int[] clouds = new int[] { 0, 1, 0, 0, 0, 1, 0 };
        Console.WriteLine(jumpingOnClouds(clouds));
    }
}