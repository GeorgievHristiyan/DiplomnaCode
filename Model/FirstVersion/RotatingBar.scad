include <CrankShaft\Gear.scad>

module RotatingBar(){

    scale([0.2,0.2,0.2])    
    rotate([0,90,0])
    translate([0,0,170])
    Gear();

    rotate([0,90,0])
    translate([0,0,-50])
    cylinder(h=100,r=3,$fn=100);

    for(i = [0:4]){
     
        rotate([0,90,0])
        translate([0,0,i*11])
        touching_wheel();
        
    }

    module touching_wheel(){
        translate([0,0,-26.35])
        cylinder(h=8.7,r=7,$fn=100);
    }
}