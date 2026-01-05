"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g = Object.create((typeof Iterator === "function" ? Iterator : Object).prototype);
    return g.next = verb(0), g["throw"] = verb(1), g["return"] = verb(2), typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (g && (g = 0, op[0] && (_ = 0)), _) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
Object.defineProperty(exports, "__esModule", { value: true });
var readline = require("readline");
var rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout,
});
var ask = function (q) {
    return new Promise(function (resolve) { return rl.question(q, resolve); });
};
var users = [];
var loggedInUser = null;
var jobs = [
    { id: 1, title: "Software Developer" },
    { id: 2, title: "System Administrator" },
    { id: 3, title: "UI/UX Designer" },
    { id: 4, title: "Data Analyst" },
];
var appliedJobs = [];
function mainMenu() {
    return __awaiter(this, void 0, void 0, function () {
        var choice;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    console.log("\n===== MAIN MENU =====");
                    console.log("1. Signup");
                    console.log("2. Login");
                    console.log("3. Exit");
                    return [4 /*yield*/, ask("Choose an option: ")];
                case 1:
                    choice = _a.sent();
                    switch (choice) {
                        case "1":
                            signup();
                            break;
                        case "2":
                            login();
                            break;
                        case "3":
                            confirmExit();
                            break;
                        default:
                            console.log("Invalid option. Try again.\n");
                            mainMenu();
                    }
                    return [2 /*return*/];
            }
        });
    });
}
function signup() {
    return __awaiter(this, void 0, void 0, function () {
        var username, firstName, lastName, gender, phone, newUser;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    console.log("\n===== SIGNUP =====");
                    return [4 /*yield*/, ask("Username: ")];
                case 1:
                    username = _a.sent();
                    return [4 /*yield*/, ask("First Name: ")];
                case 2:
                    firstName = _a.sent();
                    return [4 /*yield*/, ask("Last Name: ")];
                case 3:
                    lastName = _a.sent();
                    return [4 /*yield*/, ask("Gender: ")];
                case 4:
                    gender = _a.sent();
                    return [4 /*yield*/, ask("Phone Number: ")];
                case 5:
                    phone = _a.sent();
                    newUser = {
                        username: username,
                        firstName: firstName,
                        lastName: lastName,
                        gender: gender,
                        phone: phone,
                    };
                    users.push(newUser);
                    loggedInUser = newUser;
                    console.log("\nSignup Successful! Logged in as:", username);
                    userMenu();
                    return [2 /*return*/];
            }
        });
    });
}
function login() {
    return __awaiter(this, void 0, void 0, function () {
        var username, user;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    console.log("\n===== LOGIN =====");
                    return [4 /*yield*/, ask("Enter username: ")];
                case 1:
                    username = _a.sent();
                    user = users.find(function (u) { return u.username === username; });
                    if (!user) {
                        console.log("User not found. Try again.\n");
                        mainMenu();
                        return [2 /*return*/];
                    }
                    loggedInUser = user;
                    console.log("\nWelcome back, ".concat(user.firstName, "!"));
                    userMenu();
                    return [2 /*return*/];
            }
        });
    });
}
function userMenu() {
    return __awaiter(this, void 0, void 0, function () {
        var choice;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    console.log("\n===== USER MENU =====");
                    console.log("1. View Listed Jobs");
                    console.log("2. Apply for Job");
                    console.log("3. View Applied Jobs");
                    console.log("4. Logout");
                    return [4 /*yield*/, ask("Choose an option: ")];
                case 1:
                    choice = _a.sent();
                    switch (choice) {
                        case "1":
                            viewJobs();
                            break;
                        case "2":
                            applyForJob();
                            break;
                        case "3":
                            viewAppliedJobs();
                            break;
                        case "4":
                            logout();
                            break;
                        default:
                            console.log("Invalid option! Try again.\n");
                            userMenu();
                    }
                    return [2 /*return*/];
            }
        });
    });
}
function viewJobs() {
    console.log("\n===== AVAILABLE JOBS =====");
    jobs.forEach(function (j) { return console.log("".concat(j.id, ". ").concat(j.title)); });
    userMenu();
}
function applyForJob() {
    return __awaiter(this, void 0, void 0, function () {
        var id, jobId, job, alreadyApplied;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    console.log("\n===== APPLY FOR A JOB =====");
                    jobs.forEach(function (j) { return console.log("".concat(j.id, ". ").concat(j.title)); });
                    return [4 /*yield*/, ask("Enter job ID to apply: ")];
                case 1:
                    id = _a.sent();
                    jobId = Number(id);
                    job = jobs.find(function (j) { return j.id === jobId; });
                    if (!job) {
                        console.log("Invalid Job ID.\n");
                        return [2 /*return*/, userMenu()];
                    }
                    alreadyApplied = appliedJobs.some(function (a) { return a.username === loggedInUser.username && a.jobId === jobId; });
                    if (alreadyApplied) {
                        console.log("You already applied for this job.\n");
                        return [2 /*return*/, userMenu()];
                    }
                    appliedJobs.push({ username: loggedInUser.username, jobId: jobId });
                    console.log("Applied successfully for: ".concat(job.title, "\n"));
                    userMenu();
                    return [2 /*return*/];
            }
        });
    });
}
function viewAppliedJobs() {
    console.log("\n===== APPLIED JOBS =====");
    var userApplied = appliedJobs.filter(function (a) { return a.username === loggedInUser.username; });
    if (userApplied.length === 0) {
        console.log("No applied jobs.\n");
        return userMenu();
    }
    userApplied.forEach(function (a) {
        var job = jobs.find(function (j) { return j.id === a.jobId; });
        console.log("".concat(job.id, ". ").concat(job.title));
    });
    userMenu();
}
function logout() {
    console.log("\nLogging out ".concat(loggedInUser.username, "...\n"));
    loggedInUser = null;
    mainMenu();
}
function confirmExit() {
    return __awaiter(this, void 0, void 0, function () {
        var c;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0: return [4 /*yield*/, ask("Are you sure you want to exit? (yes/no): ")];
                case 1:
                    c = _a.sent();
                    if (c.toLowerCase() === "yes") {
                        console.log("Exiting program...");
                        rl.close();
                        process.exit(0);
                    }
                    else {
                        mainMenu();
                    }
                    return [2 /*return*/];
            }
        });
    });
}
mainMenu();
