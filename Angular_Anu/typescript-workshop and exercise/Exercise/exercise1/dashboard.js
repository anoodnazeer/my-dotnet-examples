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
var jobs = [
    { id: 1, title: "Software Developer", company: "EConcept" },
    { id: 2, title: "Web Designer", company: "Infosys" },
    { id: 3, title: "Java Developer", company: "TechnoPark" },
];
var user = {
    email: "anu@gmail.com",
    password: "anu123",
    applications: [],
};
var ask = function (question) {
    return new Promise(function (resolve) {
        rl.question(question, function (answer) { return resolve(answer); });
    });
};
function loginMenu() {
    return __awaiter(this, void 0, void 0, function () {
        var email, password;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    console.log("\n=== LOGIN MENU ===");
                    return [4 /*yield*/, ask("Email: ")];
                case 1:
                    email = _a.sent();
                    return [4 /*yield*/, ask("Password: ")];
                case 2:
                    password = _a.sent();
                    if (!(email === user.email && password === user.password)) return [3 /*break*/, 4];
                    console.log("\nLogin successful!");
                    return [4 /*yield*/, dashboardMenu()];
                case 3:
                    _a.sent();
                    return [3 /*break*/, 6];
                case 4:
                    console.log("\nInvalid credentials. Try again.");
                    return [4 /*yield*/, loginMenu()];
                case 5:
                    _a.sent();
                    _a.label = 6;
                case 6: return [2 /*return*/];
            }
        });
    });
}
function dashboardMenu() {
    return __awaiter(this, void 0, void 0, function () {
        var choice, _a, confirm_1;
        return __generator(this, function (_b) {
            switch (_b.label) {
                case 0:
                    console.log("\n=== DASHBOARD ===\n1. All Jobs\n2. My Applications\n3. Logout\n4. Exit\n");
                    return [4 /*yield*/, ask("Choose option: ")];
                case 1:
                    choice = _b.sent();
                    _a = choice;
                    switch (_a) {
                        case "1": return [3 /*break*/, 2];
                        case "2": return [3 /*break*/, 4];
                        case "3": return [3 /*break*/, 6];
                        case "4": return [3 /*break*/, 8];
                    }
                    return [3 /*break*/, 10];
                case 2: return [4 /*yield*/, showAllJobs()];
                case 3:
                    _b.sent();
                    return [3 /*break*/, 11];
                case 4: return [4 /*yield*/, showMyApplications()];
                case 5:
                    _b.sent();
                    return [3 /*break*/, 11];
                case 6:
                    console.log("Logging out...\n");
                    return [4 /*yield*/, loginMenu()];
                case 7:
                    _b.sent();
                    return [3 /*break*/, 11];
                case 8: return [4 /*yield*/, ask("Are you sure you want to exit? (yes/no): ")];
                case 9:
                    confirm_1 = _b.sent();
                    if (confirm_1.toLowerCase() === "yes" || confirm_1.toLowerCase() === "y") {
                        console.log("Exiting program...");
                        rl.close();
                        return [2 /*return*/];
                    }
                    else {
                        console.log("Exit cancelled. Returning to dashboard...");
                    }
                    return [3 /*break*/, 11];
                case 10:
                    console.log("Invalid option.");
                    _b.label = 11;
                case 11: return [4 /*yield*/, dashboardMenu()];
                case 12:
                    _b.sent(); // loop back
                    return [2 /*return*/];
            }
        });
    });
}
// async function showAllJobs() {
//   console.log("\n=== ALL JOBS ===");
//   jobs.forEach((job) => {
//     console.log(`${job.id}. ${job.title} (${job.company})`);
//   });
//   const apply = await ask("\nEnter Job ID to apply OR press Enter to go back: ");
//   if (apply.trim() !== "") {
//     const jobId = Number(apply);
//     const found = jobs.find((j) => j.id === jobId);
//     if (found) {
//       user.applications.push(jobId);
//       console.log(`Applied for: ${found.title}`);
//     } else {
//       console.log("Invalid job ID.");
//     }
//   }
// }
function showAllJobs() {
    return __awaiter(this, void 0, void 0, function () {
        var apply, jobId_1, found;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    console.log("\n=== ALL JOBS ===");
                    jobs.forEach(function (job) {
                        console.log("".concat(job.id, ". ").concat(job.title, " (").concat(job.company, ")"));
                    });
                    return [4 /*yield*/, ask("\nEnter Job ID to apply OR press Enter to go back: ")];
                case 1:
                    apply = _a.sent();
                    if (apply.trim() !== "") {
                        jobId_1 = Number(apply);
                        found = jobs.find(function (j) { return j.id === jobId_1; });
                        if (!found) {
                            console.log("Invalid job ID.");
                            return [2 /*return*/];
                        }
                        // ✅ NEW VALIDATION ADDED HERE
                        if (user.applications.includes(jobId_1)) {
                            console.log("Job already applied! Please select another one.");
                            return [2 /*return*/];
                        }
                        // Apply job if not applied before
                        user.applications.push(jobId_1);
                        console.log("Applied for: ".concat(found.title));
                    }
                    return [2 /*return*/];
            }
        });
    });
}
function showMyApplications() {
    return __awaiter(this, void 0, void 0, function () {
        return __generator(this, function (_a) {
            console.log("\n=== MY APPLICATIONS ===");
            if (user.applications.length === 0) {
                console.log("No applications found.");
                return [2 /*return*/];
            }
            user.applications.forEach(function (id) {
                var job = jobs.find(function (j) { return j.id === id; });
                if (job) {
                    console.log("".concat(job.id, ". ").concat(job.title, " (").concat(job.company, ")"));
                }
            });
            return [2 /*return*/];
        });
    });
}
loginMenu();
