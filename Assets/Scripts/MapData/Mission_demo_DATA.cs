using System.Collections.Generic;
using Map;

namespace MapData
{
    public class Mission_demo_DATA
    {

        public static HashSet<Location> unPassable = new HashSet<Location>(){
            new Location(4.5f,-1.5f),new Location(4.5f,-2.5f),
            new Location(5.5f,-2.5f),new Location(5.5f,-3.5f),
            new Location(6.5f,-1.5f),new Location(6.5f,-5.5f),new Location(6.5f,-6.5f),new Location(6.5f,-7.5f),new Location(6.5f,-8.5f),
            new Location(7.5f,-5.5f),new Location(7.5f,-6.5f),new Location(7.5f,-7.5f),new Location(7.5f,-8.5f),
            new Location(8.5f,-5.5f),new Location(8.5f,-8.5f),new Location(8.5f,-10.5f),new Location(8.5f,-11.5f),
            new Location(9.5f,-11.5f),new Location(9.5f,-10.5f),
            new Location(10.5f,-5.5f),new Location(10.5f,-4.5f),new Location(10.5f,-3.5f),
            new Location(11.5f,-3.5f),new Location(11.5f,-4.5f),new Location(11.5f,-5.5f),new Location(11.5f,-6.5f),
            new Location(12.5f,-5.5f),new Location(12.5f,-4.5f),new Location(12.5f,-3.5f),
            new Location(13.5f,-0.5f),new Location(13.5f,-1.5f),new Location(13.5f,-2.5f),
            new Location(15.5f,-0.5f),new Location(15.5f,-1.5f),new Location(15.5f,-2.5f),new Location(15.5f,-3.5f),
            new Location(15.5f,-4.5f),new Location(15.5f,-5.5f),new Location(15.5f,-6.5f),
            new Location(15.5f,-8.5f),new Location(15.5f,-9.5f),new Location(15.5f,-10.5f),
            new Location(15.5f,-11.5f),new Location(15.5f,-12.5f),new Location(15.5f,-13.5f),
            new Location(17.5f,-0.5f),new Location(17.5f,-1.5f),new Location(17.5f,-2.5f),
            new Location(18.5f,-2.5f),   new Location(20.5f,-3.5f),   new Location(25.5f,-12.5f),  new Location(27.5f,-13.5f), new Location(28.5f,-12.5f),
        };
        public static HashSet<Location> forest = new HashSet<Location>() {
            new Location(6.5f,-1.5f),new Location(14.5f,-13.5f),new Location(15.5f,-14.5f),
            new Location(16.5f,-14.5f),new Location(18.5f,-3.5f),new Location(18.5f,-2.5f),
            new Location(18.5f,-3.5f),new Location(20.5f,-2.5f),new Location(20.5f,-4.5f),
            new Location(21.5f,-3.5f),new Location(26.5f,-4.5f),new Location(26.5f,-3.5f),
            new Location(26.5f,-2.5f),new Location(26.5f,-1.5f),new Location(27.5f,-2.5f),
            new Location(27.5f,-3.5f),new Location(28.5f,-2.5f),new Location(28.5f,-1.5f),
            new Location(28.5f,-9.5f),new Location(28.5f,-10.5f),new Location(28.5f,-13.5f),
            new Location(27.5f,-14.5f),new Location(25.5f,-13.5f),new Location(26.5f,-13.5f),
            new Location(26.5f,-12.5f),new Location(27.5f,-10.5f),new Location(27.5f,-11.5f),
            new Location(27.5f,-12.5f),new Location(27.5f,-14.5f),new Location(.5f,-.5f),
            new Location(29.5f,-11.5f),
            new Location(17.5f,-8.5f),new Location(17.5f,-9.5f),new Location(17.5f,-10.5f),new Location(17.5f,-11.5f),
            new Location(18.5f,-8.5f),new Location(18.5f,-9.5f),new Location(18.5f,-10.5f),new Location(18.5f,-11.5f),
            new Location(19.5f,-8.5f),new Location(19.5f,-9.5f),new Location(19.5f,-10.5f),new Location(19.5f,-11.5f),
            new Location(20.5f,-8.5f),new Location(20.5f,-9.5f),new Location(20.5f,-10.5f),new Location(20.5f,-11.5f),
        };

    }//end Mission_demo_DATA def
}

