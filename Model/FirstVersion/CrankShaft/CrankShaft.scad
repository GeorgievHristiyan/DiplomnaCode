include <Tappet.scad>
include <Piston.scad>
include <PistonConnector.scad>
include <LeftAxis.scad>
include <RightAxis.scad>
include <PistonCylinders.scad>

module CrankShaft(){
    translate([4,-21.5,0])
    rotate([0,0,90])
    LeftAxis();
    Tappet();
    translate([3,0,0])
    rotate([0,90,0])
    Piston();
    rotate([0,0,90])
    translate([3,-4,0.4])
    PistonConnector();
    translate([4,11,4])
    rotate([0,90,0])
    Tappet();
    translate([11,11,8])
    rotate([0,90,0])
    Piston(-10,0);
    rotate([0,0,90])
    translate([14,-4,0.4])
    PistonConnector();
    translate([8,22,0])
    rotate([0,180,0])
    Tappet();
    translate([19,22,0])
    rotate([0,90,0])
    Piston();
    rotate([0,0,90])
    translate([25,-4,0.4])
    PistonConnector();
    translate([4,33,-4])
    rotate([0,270,0])
    Tappet();
    translate([11,33,-8])
    rotate([0,90,0])
    Piston();
    translate([4,49.5,0])
    rotate([0,0,-90])
    RightAxis();
    
    translate([25,30.5,0])
    rotate([90,0,270])
    Cylinders();
}
//CrankShaft();
