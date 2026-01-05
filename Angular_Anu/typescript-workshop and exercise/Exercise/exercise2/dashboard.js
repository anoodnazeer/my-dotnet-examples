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
var jobProviders = [
    "Amplitude Software solution",
    "Aitrich",
    "Econcept",
    "TechnoPark"
];
var registrations = [
    { name: "Afreeen", status: "pending" },
    { name: "Nasif", status: "on-hold" },
    { name: "Kannan", status: "candidate" },
    { name: "Meera", status: "pending" },
    { name: "Anu", status: "candidate" },
];
var ask = function (q) {
    return new Promise(function (resolve) { return rl.question(q, function (ans) { return resolve(ans); }); });
};
function login() {
    return __awaiter(this, void 0, void 0, function () {
        var email, password;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    console.log("\n===== Login =====");
                    return [4 /*yield*/, ask("Email: ")];
                case 1:
                    email = _a.sent();
                    return [4 /*yield*/, ask("Password: ")];
                case 2:
                    password = _a.sent();
                    // Simple dummy login
                    if (email === "admin@gmail.com" && password === "admin123") {
                        console.log("\nLogin Successful!\n");
                        dashboard();
                    }
                    else {
                        console.log("\nInvalid credentials. Try again.\n");
                        login();
                    }
                    return [2 /*return*/];
            }
        });
    });
}
function dashboard() {
    return __awaiter(this, void 0, void 0, function () {
        var choice;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    console.log("===== DASHBOARD =====");
                    console.log("1. Job Providers List");
                    console.log("2. New Registrations");
                    console.log("3. Registrations - Pending");
                    console.log("4. Registrations - On-Hold");
                    console.log("5. Registrations - Candidate");
                    console.log("6. Back to Dashboard (Reload)");
                    console.log("7. Exit");
                    return [4 /*yield*/, ask("Choose an option: ")];
                case 1:
                    choice = _a.sent();
                    switch (choice) {
                        case "1":
                            showJobProviders();
                            break;
                        case "2":
                            showRegistrations("all");
                            break;
                        case "3":
                            showRegistrations("pending");
                            break;
                        case "4":
                            showRegistrations("on-hold");
                            break;
                        case "5":
                            showRegistrations("candidate");
                            break;
                        case "6":
                            console.log("\nReturning to Dashboard...\n");
                            dashboard();
                            break;
                        case "7":
                            confirmExit();
                            break;
                        default:
                            console.log("Invalid option! Try again.\n");
                            dashboard();
                    }
                    return [2 /*return*/];
            }
        });
    });
}
function showJobProviders() {
    console.log("\n=== Job Providers List ===");
    jobProviders.forEach(function (p, i) { return console.log("".concat(i + 1, ". ").concat(p)); });
    console.log("");
    dashboard();
}
function showRegistrations(type) {
    console.log("\n=== Registrations (".concat(type, ") ==="));
    var list = type === "all"
        ? registrations
        : registrations.filter(function (r) { return r.status === type; });
    if (list.length === 0) {
        console.log("No records found.\n");
    }
    else {
        list.forEach(function (r, i) { return console.log("".concat(i + 1, ". ").concat(r.name, " - ").concat(r.status)); });
    }
    console.log("");
    dashboard();
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
                        console.log("\nReturning to Dashboard...\n");
                        dashboard();
                    }
                    return [2 /*return*/];
            }
        });
    });
}
login();
