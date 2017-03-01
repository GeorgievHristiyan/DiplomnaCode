module Cylinders(){
    
    translate([-33.5,-1.5,-2])
    CylindersHolder();

    for(i = [0:3]){
        translate([i*11,0,0])
        Cylinder();
        if(i != 3){
            translate([i*11,0,0])
            WallConnector();
        }
    }
    
    translate([36.5,-1.5,-2])
    CylindersHolder();
    
    module Cylinder(){
        difference(){
            cylinder(h=15,r=5,r1 =4.5,center=true,$fn=100);
            
            cylinder(h=22,r=3.5,r1 =3.0001,center=true,$fn=100);
        }
    }

    module WallConnector(){
        translate([5.5,0,0])
        cube([4,2,15],true);
    }
    
    module CylindersHolder(){
        cube([30,3,6]);
    }
}
