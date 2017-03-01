//Gear();

module Gear(){
        
    internal_diameter=20;

    //Number of Cogs.
    cogs=17;			

    //Total length of the cog. 
    cog_length=20;		

    //Width of the cogs base. 
    cog_base_width=10;	

    //30 of the cogs base. 
    cog_base_30=30; 

    //Width of the tip.
    cog_tip_width=5;    

    //30 of the tip.
    tip_30=20;	 

    //Amount of the cog length, which lacks angle, aka 30 of the cogbase.
    no_angle = 10; 

    union(){		
        difference(){
            translate ([0,0,0]){
                cylinder(30,internal_diameter,internal_diameter, $fn = 100, center=true) ;
                }

            translate ([0,0,0]){
                cylinder(30+2,0,0, $fn = 100, center=true);

            }
        }
    }

    for ( i = [0 : (cogs-1)] ){			//the spikes
        hull(){
            rotate( i * 360 / cogs, [0, 0, 1])
                translate([internal_diameter,0,0]){
                    cube ([no_angle,cog_base_width,cog_base_30],center=true);
                        translate([cog_length,0,0]) {
                            cube ([.1,cog_tip_width,tip_30],center=true);		
                        }
                }
        }
    }
}