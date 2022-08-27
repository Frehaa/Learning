#![allow(dead_code, unused_variables, unused)]

use std::mem::{ zeroed, MaybeUninit };
use std::f32::consts::PI;
use std::ffi::{c_void, OsStr} ;
use std::os::raw::c_char;
use std::os::windows::raw::HANDLE;

#[repr(C)]
#[derive(Clone, Copy)]
struct Complex {
    re: f32,
    im: f32
}

// this extern block links to the libm library
// #[link(name = "m")]
extern {
    // this is a foreign function
    // that computes the square root of a single precision complex number
    fn csqrtf(z: Complex) -> Complex;
    fn ccosf(z: Complex) -> Complex;
}

// Links with the add2 dll or lib
#[link(name="add2", kind = "static")]
extern {
    fn add2(z: i32) -> i32;
}


extern {
    fn LoadLibraryA(name: *const c_char) -> HANDLE;
    fn GetProcAddress(handle: HANDLE, name: *const c_char) -> *const c_void;
}

fn main() {
    // Uses a macro to output to the console since a function would not be safe
    println!("Hello, world!");

    // String litterals have type &str, and format! takes a string literal, but this doesn't work for some reason?
    let format_str : &str = "Testing {}: results {y}";
    // let s : String = format!(formatStr, "format", y="weird");
    let s : String = format!("Testing {}: results {y}", "format", y="weird");
    println!("{}", s);

    // Strings are heap allocated so this is like using new
    let s : String = "test".to_string();
    // Format strings are awesome though
    println!("{s:10} | wow");

    // For safety arrays have to specified their length at compile time. The compiler can usually figure it out though
    let a : [i32;5] = [1,2,3,4,5];


    // If we want to create an array uninitialized then just asked for it explicitly
    let mut b: [i32; 25] = unsafe { MaybeUninit::uninit().assume_init() };
    // b[0] = 5;
    println!("Testing {:?}", b[0]); // Who knows what value this is 

    // We can zero out values and assert equality
    let a : i32 = unsafe { zeroed() };
    assert_eq!(0, a);


    let z = Complex { re: -1., im: 0. };
    let z_sqrt = unsafe {
        csqrtf(z)
    };
    let y = Complex { re: PI, im: 0. };
    let y_cos = unsafe {
        ccosf(y)
    };
    println!(" real part {} - imaginary part {} - sqrt real {} - sqrt im {} ", z.re, z.im, z_sqrt.re, z_sqrt.im);
    println!(" real part {} - imaginary part {} - sqrt real {} - sqrt im {} ", y.re, y.im, y_cos.re, y_cos.im);

    let x : i32 = 2;
    let x_add2 : i32 = unsafe { add2(x) };
    println!("x = {} - Add2(x) = {}" , x, x_add2);

    // We can get pointers to variables
    let x : i32 = 5;
    // Since variables are default const then we need a const pointer
    let xp : *const i32 = &x;
    // unsafe { *xp = 2 }; // Does not work since xp is a const pointer
    println!("{x} {xp:?}");

    // For mutable variables we need to specify the reference as such
    let mut x : i32 = 5;
    let xp : *mut i32 = &mut x;
    let xp2 : *mut i32 = &mut x;
    unsafe { *xp = 10 };
    unsafe { *xp2 = 20 };
    println!("{x} {xp:?} {xp:?}");

    let mut big : [u8;1024] = unsafe {
        MaybeUninit::uninit().assume_init()
    };
    let mut vp : *mut c_void = unsafe { big.as_mut_ptr() as *mut c_void };

    // println!("Print uninitialized");
    // for i in big {
    //     println!("{}", i);
    // }

    big[0] = 255;
    big[1] = 255;
    big[2] = 255;
    big[3] = 255;
    big[4] = 0;
    big[5] = 1;
    big[6] = 0;
    big[7] = 0;

    let mut big2 : [u32;256];

    unsafe {
        big2 = *(vp as *mut [u32;256]);
    }
    println!("big2[0] = {} - big2[1] = {}", big2[0], big2[1]);

    
    let a : [u8;4] = [0;4]; 
    // We can do pointer arithmetic with offset
    unsafe {
        // let mut ap : *mut u8 = a as *mut u8; // Cannot because a is not primite
        // let mut ap : *mut u8 = a.as_ptr();   // Cannot because a is not mutable
        let mut ap : *mut u8 = a.as_ptr() as *mut u8;
        *ap = 5;
        ap = (ap as *mut i8).offset(2) as *mut u8;
        *ap = 10;
    }
    for v in a {
        println!("{v}");
    }

    let s = "abcd";
    println!("\nPrinting {s} as bytes");
    for b in s.as_bytes() {
        println!("{b:?}");
    }

    unsafe {

        // Maybe this works since even though &str are unicode enabled they are also 8 bytes?
        // let name : *mut c_char = "add2.dll".as_ptr() as *mut c_char;   // Does not work since it is not 0 terminated.
        let name : [c_char;9] = ['a' as i8, 'd' as i8, 'd' as i8, '2' as i8, '.' as i8, 'd' as i8, 'l' as i8, 'l' as i8, 0];
        let handle : HANDLE = LoadLibraryA(name.as_ptr());

        println!("Handle to dll {handle:?}");

        let proc_name : [c_char;5] = ['a' as i8, 'd' as i8, 'd' as i8, '2' as i8, 0];
        type IntToInt = fn(i32) -> i32;
        let proc = GetProcAddress(handle, proc_name.as_ptr());
        let proc = std::mem::transmute::<*const c_void, IntToInt>(proc);

        println!("procedure to dll {proc:?}");
        println!("procedure to dll {proc:?}");

        let x : i32 = 5;
        println!("x = {x} - proc(x) = {}", proc(x));
        
    }
}
