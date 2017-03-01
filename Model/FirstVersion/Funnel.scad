module Funnel(){
    union(){
        translate([-2,5,0])
        funnel();
        funnel_board();
    }
}
module funnel(){
    union(){
        translate([28,6,-2.1])
        difference(){
            cylinder(h=12, r1=1.2, r2=19.5, center=true, $fn=1000);
            
            cylinder(h=13, r1=0, r2=18.2, center=true, $fn=1000);
        }
        
        translate([28,6,-10.1])
        difference(){
            
            cylinder(h=4,r=1,center=true, $fn=100);
            
            cylinder(h=5,r=0.7,center=true, $fn=100);
            
            }
        }
}

module funnel_board(){

    difference(){
        translate([0,-15,-5.7])
        cube([35,50,1.5]);
        
    translate([26,11,-2.8]) 
        cylinder(h=11,r=0.3,r2=17,center=true,$fn=100);
    }
    
}