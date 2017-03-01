include <RotatingBar.scad>
include <CrankShaft\CrankShaft.scad>
include <RightWall.scad>
include <LeftWall.scad>
include <Funnel.scad>
include <BottomWall.scad>
include <FillamentBar.scad>

rotate([0,0,90])
translate([9,-23,70])
FillamentBar();

rotate([0,0,90])
translate([-44,-58,-50])
RightWall();

rotate([0,0,270])
translate([-9,44,25])
RotatingBar();
translate([0,-5,25])
CrankShaft();

rotate([0,0,90])
translate([56,-58,-50])
LeftWall();

scale([2,2,2])
translate([-6,-5.5,0])
Funnel();

rotate([0,0,90])
translate([-44,-58,-50])
BottomWall();