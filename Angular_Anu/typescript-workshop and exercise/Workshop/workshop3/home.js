"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var readline = require("readline");
var rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
});
var loggedInUser = null;
var registeredUsers = [];
//initial menu
var initialMenu = function () {
    console.log('Welcome! Please select an option:');
    console.log('1. Register');
    console.log('2. Login');
    console.log('3. Exit');
    rl.question('Enter your choice: ', function (choice) {
        if (choice === '1') {
            register();
        }
        else if (choice === '2') {
            login();
        }
        else if (choice === '3') {
            exit();
        }
        else {
            console.log('Invalid choice. Please try again.');
            initialMenu();
        }
    });
};
// Register function
var register = function () {
    console.log('Registration:');
    rl.question('First name: ', function (firstName) {
        rl.question('Last name: ', function (lastName) {
            rl.question('Email: ', function (email) {
                rl.question('Phone number: ', function (phoneNumber) {
                    rl.question('Password: ', function (password) {
                        rl.question('Confirm password: ', function (confirmPassword) {
                            rl.question('Gender: ', function (gender) {
                                // Perform registration logic here
                                var newUser = {
                                    firstName: firstName,
                                    lastName: lastName,
                                    email: email,
                                    phoneNumber: phoneNumber,
                                    password: password,
                                    gender: gender
                                };
                                registeredUsers.push(newUser);
                                console.log('Registration successful!');
                                redirectMenu();
                            });
                        });
                    });
                });
            });
        });
    });
};
// Redirect function
var redirectMenu = function () {
    rl.question('Redirect to previous menu? (Y/N): ', function (choice) {
        if (choice.toLowerCase() === 'y') {
            initialMenu();
        }
        else if (choice.toLowerCase() === 'n') {
            exit();
        }
        else {
            console.log('Invalid choice. Exiting...');
            exit();
        }
    });
};
// Login function
var login = function () {
    console.log('Login:');
    rl.question('Email: ', function (email) {
        rl.question('Password: ', function (password) {
            // Perform login logic here
            var user = registeredUsers.find(function (u) { return u.email === email && u.password === password; });
            if (user) {
                console.log('Login successful!');
                loggedInUser = user;
                loggedInMenu();
            }
            else {
                console.log('Invalid email or password. Please try again.');
                login();
            }
        });
    });
};
// Logged-in menu options
var loggedInMenu = function () {
    console.log('1. viewprofile');
    console.log('2. Update Profile');
    console.log('3. Logout');
    rl.question('Enter your choice: ', function (choice) {
        if (choice === '1') {
            viewProfile();
        }
        else if (choice === '2') {
            updateProfile();
        }
        else if (choice === '3') {
            logout();
        }
        else {
            console.log('Invalid choice. Please try again.');
            loggedInMenu();
        }
    });
};
// Update profile function
// const updateProfile = (): void => {
//     console.log('Update Profile:');
//     rl.question('First name: ', (firstName: string) => {
//       rl.question('Last name: ', (lastName: string) => {
//         rl.question('Email: ', (email: string) => {
//           rl.question('Phone number: ', (phoneNumber: string) => {
//             rl.question('Password: ', (password: string) => {
//               rl.question('Confirm password: ', (confirmPassword: string) => {
//                 rl.question('Gender: ', (gender: string) => {
//                   // Perform update profile logic here
//                   loggedInUser = {
//                     ...loggedInUser,
//                     firstName,
//                     lastName,
//                     email,
//                     phoneNumber,
//                     password,
//                     gender
//                   };
//                   console.log('Profile updated successfully!');
//                   loggedInMenu();
//                 });
//               });
//             });
//           });
//         });
//       });
//     });
//   };
var updateProfile = function () {
    console.log('Update Profile:');
    rl.question('First name: ', function (firstName) {
        rl.question('Last name: ', function (lastName) {
            rl.question('Email: ', function (email) {
                rl.question('Phone number: ', function (phoneNumber) {
                    rl.question('Password: ', function (password) {
                        rl.question('Confirm password: ', function (confirmPassword) {
                            if (password !== confirmPassword) {
                                console.log('Passwords do not match. Try again.');
                                return updateProfile();
                            }
                            rl.question('Gender: ', function (gender) {
                                if (loggedInUser) {
                                    var index = registeredUsers.findIndex(function (u) { return u.email === loggedInUser.email; });
                                    registeredUsers[index] = {
                                        firstName: firstName,
                                        lastName: lastName,
                                        email: email,
                                        phoneNumber: phoneNumber,
                                        password: password,
                                        gender: gender
                                    };
                                    loggedInUser = registeredUsers[index]; // Update session
                                }
                                console.log('Profile updated successfully!');
                                loggedInMenu();
                            });
                        });
                    });
                });
            });
        });
    });
};
// Logout function
var logout = function () {
    console.log('Logging out...');
    loggedInUser = null;
    initialMenu();
};
// Exit function
var exit = function () {
    console.log('Exiting...');
    rl.close();
};
var viewProfile = function () {
    if (!loggedInUser) {
        console.log("No user is logged in.");
        loggedInMenu();
        return;
    }
    console.log('\n--- Your Profile ---');
    console.log("First Name   : ".concat(loggedInUser.firstName));
    console.log("Last Name    : ".concat(loggedInUser.lastName));
    console.log("Email        : ".concat(loggedInUser.email));
    console.log("Phone Number : ".concat(loggedInUser.phoneNumber));
    console.log("Gender       : ".concat(loggedInUser.gender));
    console.log('----------------------\n');
    loggedInMenu();
};
// Start the initial menu
initialMenu();
