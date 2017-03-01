include <LeftWall.scad>
include <MaterialBar.scad>
include <Stand.scad>
include <RightWall.scad>
include <BottomWall.scad>

LeftWall();

for(i = [0:4]){
    translate([i*40.5,30,112])
    MaterialBar();
    
    if(i < 4){
        translate([i*40.5,0,30])
        Stand();
    }
}

translate([201.5,0,0])
RightWall();

translate([0,0,30])
BottomWall();