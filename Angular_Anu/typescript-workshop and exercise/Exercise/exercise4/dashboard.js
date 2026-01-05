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
var adminUsername = "admin";
var adminPassword = "admin123";
var jobs = [];
var nextJobId = 1;
function login() {
    return __awaiter(this, void 0, void 0, function () {
        var username, password;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    console.log("\n===== LOGIN =====");
                    return [4 /*yield*/, ask("Enter username: ")];
                case 1:
                    username = _a.sent();
                    return [4 /*yield*/, ask("Enter password: ")];
                case 2:
                    password = _a.sent();
                    if (username === adminUsername && password === adminPassword) {
                        console.log("\nLogin Successful!\n");
                        jobMenu();
                    }
                    else {
                        console.log("\nInvalid username/password. Try again.\n");
                        login();
                    }
                    return [2 /*return*/];
            }
        });
    });
}
function jobMenu() {
    return __awaiter(this, void 0, void 0, function () {
        var choice;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    console.log("===== JOB MENU =====");
                    console.log("1. Post Job");
                    console.log("2. Job List");
                    console.log("3. Remove Job");
                    console.log("4. Search Job");
                    console.log("5. Logout");
                    return [4 /*yield*/, ask("Choose an option: ")];
                case 1:
                    choice = _a.sent();
                    switch (choice) {
                        case "1":
                            postJob();
                            break;
                        case "2":
                            jobList();
                            break;
                        case "3":
                            removeJob();
                            break;
                        case "4":
                            searchJob();
                            break;
                        case "5":
                            logout();
                            break;
                        default:
                            console.log("Invalid option! Try again.\n");
                            jobMenu();
                    }
                    return [2 /*return*/];
            }
        });
    });
}
function postJob() {
    return __awaiter(this, void 0, void 0, function () {
        var title, description, newJob;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    console.log("\n===== POST NEW JOB =====");
                    return [4 /*yield*/, ask("Enter job title: ")];
                case 1:
                    title = _a.sent();
                    return [4 /*yield*/, ask("Enter job description: ")];
                case 2:
                    description = _a.sent();
                    newJob = {
                        id: nextJobId++,
                        title: title,
                        description: description,
                    };
                    jobs.push(newJob);
                    console.log("\nJob posted successfully!\n");
                    jobMenu();
                    return [2 /*return*/];
            }
        });
    });
}
function jobList() {
    console.log("\n===== JOB LIST =====");
    if (jobs.length === 0) {
        console.log("No jobs posted yet.\n");
        return jobMenu();
    }
    jobs.forEach(function (job) {
        return console.log("".concat(job.id, ". ").concat(job.title, " - ").concat(job.description));
    });
    console.log("");
    jobMenu();
}
function removeJob() {
    return __awaiter(this, void 0, void 0, function () {
        var key, removed, id, index, index;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    console.log("\n===== REMOVE JOB =====");
                    return [4 /*yield*/, ask("Enter Job ID or Title to remove: ")];
                case 1:
                    key = _a.sent();
                    removed = false;
                    id = Number(key);
                    if (!isNaN(id)) {
                        index = jobs.findIndex(function (j) { return j.id === id; });
                        if (index !== -1) {
                            jobs.splice(index, 1);
                            removed = true;
                        }
                    }
                    if (!removed) {
                        index = jobs.findIndex(function (j) { return j.title.toLowerCase() === key.toLowerCase(); });
                        if (index !== -1) {
                            jobs.splice(index, 1);
                            removed = true;
                        }
                    }
                    if (removed) {
                        console.log("Job removed successfully!\n");
                    }
                    else {
                        console.log("Job not found.\n");
                    }
                    jobMenu();
                    return [2 /*return*/];
            }
        });
    });
}
function searchJob() {
    return __awaiter(this, void 0, void 0, function () {
        var key, results, id, job, titleMatch;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    console.log("\n===== SEARCH JOB =====");
                    return [4 /*yield*/, ask("Enter Job ID or Title to search: ")];
                case 1:
                    key = _a.sent();
                    results = [];
                    id = Number(key);
                    if (!isNaN(id)) {
                        job = jobs.find(function (j) { return j.id === id; });
                        if (job)
                            results.push(job);
                    }
                    titleMatch = jobs.filter(function (j) { return j.title.toLowerCase().includes(key.toLowerCase()); });
                    results.push.apply(results, titleMatch);
                    results = Array.from(new Set(results));
                    if (results.length === 0) {
                        console.log("No job found.\n");
                    }
                    else {
                        console.log("\nSearch Results:");
                        results.forEach(function (job) {
                            return console.log("".concat(job.id, ". ").concat(job.title, " - ").concat(job.description));
                        });
                        console.log("");
                    }
                    jobMenu();
                    return [2 /*return*/];
            }
        });
    });
}
function logout() {
    console.log("\nLogging out... Goodbye!\n");
    rl.close();
    process.exit(0);
}
login();
