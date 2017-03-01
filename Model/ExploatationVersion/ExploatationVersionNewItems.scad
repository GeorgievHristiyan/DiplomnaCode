import("C:/Users/Lubo/Downloads/ExploatationVersion (1).stl");


for(i = [1:5]){
    translate([-10,24,80])
    translate([i*41,0,0])
    StepMotor();
    translate([5,20,80])
    translate([i*41,0,0])
    InfraRed();
}



module InfraRed(){
scale([0.1,0.1,0.1])
rotate([0,0,270])
translate([-125,-175,90])
text("Infrared sensor");

color("blue")
translate([-20,2,4])
cube([5,13,5]);
}

module StepMotor(){
scale([0.1,0.1,0.1])
rotate([360,0,0])
translate([-200,40,90])
text("Step motor");

color("red")
translate([-20,2,4])
cube([6.8,5,5]);
}