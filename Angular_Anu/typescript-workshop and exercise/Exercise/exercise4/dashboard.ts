import * as readline from "readline";

const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout,
});

 

const ask = (q: string): Promise<string> => {
  return new Promise((resolve) => rl.question(q, resolve));
};

 

const adminUsername = "admin";
const adminPassword = "admin123";

 

interface Job {
  id: number;
  title: string;
  description: string;
}

let jobs: Job[] = [];
let nextJobId = 1;

 

async function login() {
  console.log("\n===== LOGIN =====");

  const username = await ask("Enter username: ");
  const password = await ask("Enter password: ");

  if (username === adminUsername && password === adminPassword) {
    console.log("\nLogin Successful!\n");
    jobMenu();
  } else {
    console.log("\nInvalid username/password. Try again.\n");
    login();
  }
}

 

async function jobMenu() {
  console.log("===== JOB MENU =====");
  console.log("1. Post Job");
  console.log("2. Job List");
  console.log("3. Remove Job");
  console.log("4. Search Job");
  console.log("5. Logout");

  const choice = await ask("Choose an option: ");

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
}

 

async function postJob() {
  console.log("\n===== POST NEW JOB =====");

  const title = await ask("Enter job title: ");
  const description = await ask("Enter job description: ");

  const newJob: Job = {
    id: nextJobId++,
    title,
    description,
  };

  jobs.push(newJob);

  console.log("\nJob posted successfully!\n");
  jobMenu();
}

 

function jobList() {
  console.log("\n===== JOB LIST =====");

  if (jobs.length === 0) {
    console.log("No jobs posted yet.\n");
    return jobMenu();
  }

  jobs.forEach((job) =>
    console.log(`${job.id}. ${job.title} - ${job.description}`)
  );

  console.log("");
  jobMenu();
}

 

async function removeJob() {
  console.log("\n===== REMOVE JOB =====");
  const key = await ask("Enter Job ID or Title to remove: ");

  let removed = false;

   
  const id = Number(key);
  if (!isNaN(id)) {
    const index = jobs.findIndex((j) => j.id === id);
    if (index !== -1) {
      jobs.splice(index, 1);
      removed = true;
    }
  }

  
  if (!removed) {
    const index = jobs.findIndex((j) => j.title.toLowerCase() === key.toLowerCase());
    if (index !== -1) {
      jobs.splice(index, 1);
      removed = true;
    }
  }

  if (removed) {
    console.log("Job removed successfully!\n");
  } else {
    console.log("Job not found.\n");
  }

  jobMenu();
}

 

async function searchJob() {
  console.log("\n===== SEARCH JOB =====");
  const key = await ask("Enter Job ID or Title to search: ");

  let results: Job[] = [];

  
  const id = Number(key);
  if (!isNaN(id)) {
    const job = jobs.find((j) => j.id === id);
    if (job) results.push(job);
  }

   
  const titleMatch = jobs.filter(
    (j) => j.title.toLowerCase().includes(key.toLowerCase())
  );
  results.push(...titleMatch);

   
  results = Array.from(new Set(results));

  if (results.length === 0) {
    console.log("No job found.\n");
  } else {
    console.log("\nSearch Results:");
    results.forEach((job) =>
      console.log(`${job.id}. ${job.title} - ${job.description}`)
    );
    console.log("");
  }

  jobMenu();
}

 

function logout() {
  console.log("\nLogging out... Goodbye!\n");
  rl.close();
  process.exit(0);
}

 

login();
