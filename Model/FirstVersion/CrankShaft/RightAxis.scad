module RightAxis(){ 
    include<LeftAxis.scad>
    include<Gear.scad> 
    scale([0.2,0.2,0.2])
    rotate([0,90,0])
    translate([0,0,20])
    Gear();
    LeftAxis();    
}
