module Piston(){
    translate([0,-2.5,3])
     rotate([0,180,0])
     union(){    
            
            //toppom cylinder
            difference(){
                translate([0,0,10])
                rotate([90,0,0])
                cylinder (h = 2.9, r=1.2, center = true, $fn= 100);

                translate([0,0,10])
                rotate([90,0,0])
                cylinder (h = 6, r=0.7, center = true, $fn= 100);
            }
            
            //middle cylinder
            difference(){
                cylinder (h = 20, r=1, center = true, $fn=100);
                              translate([0,0,10])
                rotate([90,0,0])
                cylinder (h = 6, r=0.7, center = true, $fn= 100);
                
         translate([0,0,-8.5]) 
         rotate([90,0,0])
         cylinder(h=2,r=0.3, center=true,$fn=100);        
            }
            
         #translate([0,0,-8.5])  
         rotate([90,0,0])
         cylinder(h=3,r=0.28, center=true,$fn=100);  
            
              
            //bottom cylinder
            difference(){
                translate([0,0,-9.5])
                cylinder (h = 10, r=3, 
                center = true, $fn=100);
              
              translate([0,0,-6.8])
                cube([4,2.03,7],true); 
             
                
            }
            rotate([0,90,0])    
            translate([14.5,0,0])
            cube([5,2,5.6],true);
        }   
}
