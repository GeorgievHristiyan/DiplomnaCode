include<TopWall.scad>
include<Barrel.scad>
include<Left_Right_Wall.scad>
include<Board.scad>
include<Left_Right_Wall.scad>


rotate([0,180,0])
translate([15,0,-30])
Top_Wall();

translate([-15,0,0.7])
Barrel();

rotate([0,90,0])
translate([0,0,-30])
Left_Right_Wall();

translate([-15,0,-10])
Board();

rotate([-90,90,90])
Left_Right_Wall();
