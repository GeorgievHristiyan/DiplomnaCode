//Left_Right_Wall();
module Left_Right_Wall(){
    difference(){
        cube(size = [60,33,3], center = true);  
        
        rotate([0,90,0])
        translate([-2,0,10])
        #cube(size = [5,30,1.1], center = true);  
        
        translate([10,5,0])
        cylinder(h=5, r=0.2,center=true, $fn=1000);
        
        translate([10,-5,0])
        cylinder(h=5, r=0.2,center=true, $fn=1000);
    }
}