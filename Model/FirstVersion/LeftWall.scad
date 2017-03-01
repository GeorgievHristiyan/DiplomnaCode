module LeftWall(){
    union(){
        difference(){
            cube([6,70,140]);
            
            rotate([0,90,0])
            translate([-120,35,-5.5])
            cylinder(h=20, r=10,center=true, $fn=5);
            
            #rotate([0,90,0])
            translate([-75,14,1])
            cylinder(h=5, r=3.1,center=true, $fn=100);
          
            #translate([-0.5,30.9,73.4])
            cube([3.5,6.2,3.2]);
            
            #rotate([0,90,0])
            translate([-75,54,1])
            cylinder(h=5, r=2.1,center=true, $fn=100);
            
            door();
        }
        
        rotate([0,90,0])
        translate([-90,60,-12])
        cube([2,7,12]);
        
        color("blue");
        translate([1.5,24,128.1])
        cube([2.9,4.9,12]);
    }
}


module door(){
    
    translate([-2,26,128])
    cube([10,1,15]);
    
    translate([1.4,26,128.1])
    cube([3.1,3,14]);
    
        
    translate([-2,44,128.1])
    cube([10,1,15]);
    
    translate([-2,26,128])
    cube([10,19,1]);

}