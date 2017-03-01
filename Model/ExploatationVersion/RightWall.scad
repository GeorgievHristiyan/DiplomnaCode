RightWall();
module RightWall(){
    difference(){
        cube([5,60,120]);        
            
    translate([-0.5,24.9,107])
        cube([3.5,10.2,15]);
        
        #rotate([0,90,0])
        translate([-40,23,-3])
 cylinder(6,1, $fn=100);
        
        #rotate([0,90,0])
        translate([-40,37,-3])
 cylinder(6,1, $fn=100);
    }
}