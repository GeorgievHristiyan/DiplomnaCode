module Tappet(){
    scale([1,1,1])
    rotate([0,90,0])
    union(){ 
        sml();
        connector();
        translate([0,-5,0])
        rotate([0,0,180])
        sml();
        
    }


    module connector(){
       translate([0,-2.5,-4])
        rotate([90,0,0])
        cylinder (h = 6, r=0.68, center = true, $fn=100);
    }

    module sml(){
        
        union(){
            //toppom cylinder
            translate([0,0,-4])
            rotate ([90,0,0])
            cylinder (h = 2, r=1, center = true, $fn=100);
       
        //middle cylinder
         //   difference(){
            cylinder (h = 8, r=1, center = true, $fn=100);
             
            //bottom cylinder
            translate([0,1,4])
            rotate ([90,0,0])
            difference(){
                cylinder (h = 4, r=1, center = true, $fn=100);
                    
                translate([0,0,-2])
                rotate ([0,0,0]) 
                cylinder (h = 4, r=0.6, center = true, $fn=10);
            }
        }
    }
}