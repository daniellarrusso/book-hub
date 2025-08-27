# BookHub – Design Notes & Trade-offs

This document outlines some of the design decisions made during the project along with possible improvements and trade-offs considered.

---

## Design Trade-offs

### 1. Image Handling
- **Current Approach:** Placeholder images are served from [Picsum](https://picsum.photos/).  
- **Pros:**  
  - Quick to implement  
  - No storage overhead in the project repo  
- **Cons:**  
  - Reliance on a third-party service  
  - No control over caching or image optimization  

- **Future Approach:**  
  - Store book cover images in an object store (e.g., AWS S3, Azure Blob).  
  - Use a **CDN with caching** (CloudFront, Azure CDN) to reduce latency.  
  - Support lazy loading for performance.  

---

### 2. State Management vs. Refetching
- **Current Approach:** After each create/update/delete operation, the app **refetches all books** from the API.  
- **Pros:**  
  - Guarantees data consistency with the backend  
  - Simple logic, avoids complex state bugs  
- **Cons:**  
  - Extra network calls  
  - Not as efficient for larger datasets  

- **Alternative Approach:**  
  - Update the local Vue state directly (optimistic updates).  
  - Use Pinia for centralized state management.  

---

### 3. Database Choice
- **Current Approach:** Using SQLite for ease of setup.  
- **Pros:**  
  - No external dependency, portable  
  - Quick to develop and demo  
- **Cons:**  
  - Limited concurrency handling  
  - Not suitable for large-scale use  
- **Future Approach:**  
  - Replace with SQL Server or other suitable RDMS for scalability. 

---


- **General Commentary:**  
  For a small demo project, I thought that the consistenchy offered refetching outweighed the costs - especially when fetching paged responses from the backend - pagination would also need to be reflected in the UI update approach. In a production scenario with high traffic, local state updates + background refresh would be more efficient. 

  In retrospect, I could have handled all the pagination in the client and have the backend serve ALL the books to the frontend. 

  Ultimately ran out of time, but given more:

  - The Analytics page is currently a bit basic; there was potential form additional visualizations and interactivity to make it more insightful.
  - The main BookList component ended up handling quite a lot of logic (too smart), and a refactor would likely be beneficial to simplify it and improve maintainability.
  - It felt like there were further requirements to be understood around Note Status and the act of taing notes / comments. Potentially some sort of workflow scenario to move notes on to certain stages.
  - See next steps

---

## Next Steps
- Replace placeholder images with a proper upload + CDN caching flow.  
- Introduce client-side state management for better efficiency at scale.  
- Understand requirements around data visualisation for Analytics page
- Understand requirements about potential Note Status stages and workflow element
- Consider a production-ready database backend.  
- Further Unit and Integration and e2e testing 
- Model: Split out Author into a separate table to allow selection or creation of authors.

---

## Visual Improvements
- **Accessibility:** Basic form validation is in place, but improvements such as ARIA labels and keyboard navigation support would make the app more inclusive.  
- **Responsiveness:** The app adapts to smaller screens, but layouts could be refined further.  

