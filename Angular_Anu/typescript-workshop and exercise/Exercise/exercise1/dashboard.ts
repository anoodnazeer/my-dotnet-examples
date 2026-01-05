import * as readline from "readline";

const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout,
});

interface User {
  email: string;
  password: string;
  applications: number[];
}

interface Job {
  id: number;
  title: string;
  company: string;
}

const jobs: Job[] = [
  { id: 1, title: "Software Developer", company: "EConcept" },
  { id: 2, title: "Web Designer", company: "Infosys" },
  { id: 3, title: "Java Developer", company: "TechnoPark" },
];

const user: User = {
  email: "anu@gmail.com",
  password: "anu123",
  applications: [],
};

const ask = (question: string): Promise<string> => {
  return new Promise((resolve) => {
    rl.question(question, (answer) => resolve(answer));
  });
};

async function loginMenu() {
  console.log("\n=== LOGIN MENU ===");
  const email = await ask("Email: ");
  const password = await ask("Password: ");

  if (email === user.email && password === user.password) {
    console.log("\nLogin successful!");
    await dashboardMenu();
  } else {
    console.log("\nInvalid credentials. Try again.");
    await loginMenu();
  }
}

async function dashboardMenu() {
  console.log(`
=== DASHBOARD ===
1. All Jobs
2. My Applications
3. Logout
4. Exit
`);

 const choice = await ask("Choose option: ");

  switch (choice) {
    case "1":
      await showAllJobs();
      break;
    case "2":
      await showMyApplications();
      break;
    case "3":
      console.log("Logging out...\n");
      await loginMenu();
      break;
      case "4":
  const confirm = await ask("Are you sure you want to exit? (yes/no): ");
  
  if (confirm.toLowerCase() === "yes" || confirm.toLowerCase() === "y") {
    console.log("Exiting program...");
    rl.close();
    return;
  } else {
    console.log("Exit cancelled. Returning to dashboard...");
  }
  break;
    //case "4":
    //   console.log("Exiting program...");
    //   rl.close();
    //   return;
    
    default:
      console.log("Invalid option.");
  }

  await dashboardMenu(); // loop back
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

async function showAllJobs() {
  console.log("\n=== ALL JOBS ===");
  jobs.forEach((job) => {
    console.log(`${job.id}. ${job.title} (${job.company})`);
  });

  const apply = await ask("\nEnter Job ID to apply OR press Enter to go back: ");

  if (apply.trim() !== "") {
    const jobId = Number(apply);
    const found = jobs.find((j) => j.id === jobId);

    if (!found) {
      console.log("Invalid job ID.");
      return;
    }

    // ✅ NEW VALIDATION ADDED HERE
    if (user.applications.includes(jobId)) {
      console.log("Job already applied! Please select another one.");
      return;
    }

    // Apply job if not applied before
    user.applications.push(jobId);
    console.log(`Applied for: ${found.title}`);
  }
}


async function showMyApplications() {
  console.log("\n=== MY APPLICATIONS ===");

  if (user.applications.length === 0) {
    console.log("No applications found.");
    return;
  }

  user.applications.forEach((id) => {
    const job = jobs.find((j) => j.id === id);
    if (job) {
      console.log(`${job.id}. ${job.title} (${job.company})`);
    }
  });
}

loginMenu();